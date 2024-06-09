using Hirundo.Commons.Models;
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

    public HirundoApp(
        IComputedValuesCalculatorBuilder calculatorBuilder,
        IObservationConditionsBuilder observationsBuilder,
        IReturningSpecimenConditionsBuilder returningBuilder,
        IPopulationProcessorBuilder populationProcessorBuilder,
        IStatisticsProcessorBuilder statisticsProcessorBuilder,
        ISummaryWriterBuilder summaryWriterBuilder
    )
    {
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

        var observations = GetObservations(applicationConfig, token);
        Log.Information($"Wybrano {observations.Length} obserwacji.");

        Log.Information("Łączenie obserwacji w osobniki...");

        var specimens = GetSpecimens(observations);

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

        var summary = ProcessSummaries(returningSpecimens, summaryProcessor);

        var results = new ReturningSpecimensResults
        {
            Results = summary,
            Explanation = applicationConfig.Explain()
        };

        Log.Information($"Przygotowano {summary.Count} wierszy danych wynikowych.");

        resultsWriter.Write(results);

        foreach (var writer in applicationConfig.Results.Writers)
        {
            Log.Information($"Zapisano dane wynikowe do pliku {writer.Path}.");
        }
    }

    private Observation[] GetObservations(ApplicationParameters applicationConfig, CancellationToken? token)
    {
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

        var observations = applicationConfig.Databases.BuildDataSource(token).GetObservations().ToArray();

        Log.Information($"Odczytano {observations.Length} obserwacji. Wyliczanie dodatkowych wartości...");

        observations = observations
            .Select(computedValuesCalculator.Calculate)
            .ToArray();

        Log.Information("Filtrowanie obserwacji po warunkach...");

        var selectedObservations = observations.Where(observationConditions.IsAccepted).ToArray();
        return selectedObservations;
    }

    private static List<ReturningSpecimenSummary> ProcessSummaries(Specimen[] returningSpecimens, ISummaryProcessor summaryProcessor)
    {
        var summary = new List<ReturningSpecimenSummary>();

        var total = returningSpecimens.Length;
        Log.Information("Przetwarzanie danych {total} powracających osobników.", total);

        foreach (var returningSpecimen in returningSpecimens)
        {
            var index = Array.IndexOf(returningSpecimens, returningSpecimen) + 1;
            Log.Information("Obliczanie statystyk: {index}/{total}.", index, total);
            var summaryResult = summaryProcessor.GetSummary(returningSpecimen);
            summary.Add(summaryResult);
        }

        Log.Information("Przetworzono dane statystyczne {total} powracających osobników.", total);

        return summary;
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