﻿using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;
using Hirundo.Databases;
using Hirundo.Processors.Computed;
using Hirundo.Processors.Observations;
using Hirundo.Processors.Population;
using Hirundo.Processors.Returning;
using Hirundo.Processors.Statistics;
using Hirundo.Processors.Summary;
using Hirundo.Writers;
using Serilog;

namespace Hirundo.App;

public class HirundoApp : IHirundoApp
{
    private readonly IDatabaseBuilder _databaseBuilder = new DatabaseBuilder();
    private readonly IComputedValuesCalculatorBuilder _calculatorBuilder = new ComputedValuesCalculatorBuilder();
    private readonly IObservationConditionsBuilder _observationConditionsBuilder = new ObservationConditionsBuilder();
    private readonly IPopulationProcessorBuilder _populationProcessorBuilder = new PopulationProcessorBuilder();
    private readonly IReturningSpecimenConditionsBuilder _returningSpecimenConditionsBuilder = new ReturningSpecimenConditionsBuilder();
    private readonly IStatisticsProcessorBuilder _statisticsProcessorBuilder = new StatisticsProcessorBuilder();
    private readonly SummaryProcessorBuilder _summaryProcessorBuilder = new();
    private readonly ISummaryWriterBuilder _summaryWriterBuilder = new SummaryWriterBuilder();

    public HirundoApp()
    {
    }

    public HirundoApp(IDatabaseBuilder databaseBuilder, ISummaryWriterBuilder summaryWriterBuilder)
    {
        _databaseBuilder = databaseBuilder;
        _summaryWriterBuilder = summaryWriterBuilder;
    }

    public HirundoApp(
        IDatabaseBuilder databaseBuilder,
        IComputedValuesCalculatorBuilder calculatorBuilder,
        IObservationConditionsBuilder observationsBuilder,
        IReturningSpecimenConditionsBuilder returningBuilder,
        IPopulationProcessorBuilder populationProcessorBuilder,
        IStatisticsProcessorBuilder statisticsProcessorBuilder,
        ISummaryWriterBuilder summaryWriterBuilder
    )
    {
        _databaseBuilder = databaseBuilder;
        _calculatorBuilder = calculatorBuilder;
        _observationConditionsBuilder = observationsBuilder;
        _returningSpecimenConditionsBuilder = returningBuilder;
        _populationProcessorBuilder = populationProcessorBuilder;
        _statisticsProcessorBuilder = statisticsProcessorBuilder;
        _summaryWriterBuilder = summaryWriterBuilder;
    }

    public void Run(ApplicationParameters applicationConfig, CancellationToken? token = null)
    {
        ArgumentNullException.ThrowIfNull(applicationConfig);

        var database = _databaseBuilder
            .NewBuilder()
            .WithDatabaseParameters([.. applicationConfig.Databases.Databases])
            .WithCancellationToken(token)
            .Build();

        var observationConditions = _observationConditionsBuilder
            .NewBuilder()
            .WithObservationConditions(applicationConfig.Observations.Conditions)
            .WithCancellationToken(token)
            .Build();

        var computedValuesCalculator = _calculatorBuilder
            .NewBuilder()
            .WithComputedValues(applicationConfig.ComputedValues.ComputedValues)
            .WithCancellationToken(token)
            .Build();

        var returningSpecimenConditions = _returningSpecimenConditionsBuilder
            .NewBuilder()
            .WithReturningSpecimensConditions(applicationConfig.ReturningSpecimens.Conditions)
            .WithCancellationToken(token)
            .Build();

        var populationProcessor = _populationProcessorBuilder
            .NewBuilder()
            .WithPopulationConditions(applicationConfig.Population.Conditions)
            .WithCancellationToken(token)
            .Build();

        var statisticsProcessor = _statisticsProcessorBuilder
            .NewBuilder()
            .WithStatisticsOperations(applicationConfig.Statistics.Operations)
            .WithCancellationToken(token)
            .Build();

        using var resultsWriter = _summaryWriterBuilder
            .NewBuilder()
            .WithWriterParameters(applicationConfig.Results.Writers)
            .WithCancellationToken(token)
            .Build();

        var observations = database.GetObservations().ToArray();

        Log.Information($"Odczytano {observations.Length} obserwacji. Wyliczanie dodatkowych wartości...");

        observations = observations
            .Select(computedValuesCalculator.Calculate)
            .ToArray();

        Log.Information("Filtrowanie obserwacji po warunkach...");

        var selectedObservations = observations.Where(observationConditions.IsAccepted).ToArray();

        Log.Information($"Wybrano {selectedObservations.Length} obserwacji.");

        Log.Information("Łączenie obserwacji w osobniki...");

        Specimen[] specimens = GetSpecimens(selectedObservations);

        Log.Information($"Wybrano {specimens.Length} osobników.");

        Log.Information("Wybieranie powracających osobników...");

        var returningSpecimens = specimens.Where(returningSpecimenConditions.IsReturning).ToArray();

        Log.Information($"Wybrano {returningSpecimens.Length} powracających osobników.");

        Log.Information("Przetwarzanie obliczeń...");

        var summaryProcessor = _summaryProcessorBuilder
            .WithPopulationProcessor(populationProcessor)
            .WithStatisticsProcessor(statisticsProcessor)
            .WithTotalPopulation(specimens)
            .Build();

        var summary = returningSpecimens
            .Select(summaryProcessor.GetSummary)
            .ToList();

        var results = new ReturningSpecimensResults
        {
            Results = summary,
            Explanation = ExplainerHelpers.Explain(applicationConfig)
        };

        Log.Information($"Przygotowano {summary.Count} wierszy danych wynikowych.");

        resultsWriter.Write(results);

        foreach (var writer in applicationConfig.Results.Writers)
        {
            Log.Information($"Zapisano dane wynikowe do pliku {writer.Path}.");
        }
    }

    private static Specimen[] GetSpecimens(Observation[] selectedObservations)
    {
        return selectedObservations
            .Where(observation => !string.IsNullOrWhiteSpace(observation.Ring))
            .GroupBy(observation => observation.Ring)
            .Select(group => new Specimen(group.Key, [.. group.OrderBy(o => o.Date)]))
            .ToArray();
    }
}