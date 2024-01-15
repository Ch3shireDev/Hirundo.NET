using Hirundo.Databases;
using Hirundo.Processors.Observations.Conditions;
using Hirundo.Processors.Population;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.Specimens;
using Hirundo.Processors.Statistics;
using Hirundo.Processors.Summary;
using Hirundo.Writers.Summary;
using Serilog;

namespace Hirundo.Configuration;

public class HirundoApp
{
    private readonly DatabaseBuilder databaseBuilder = new();
    private readonly ObservationFiltersBuilder observationFiltersBuilder = new();
    private readonly PopulationProcessorBuilder populationProcessorBuilder = new();
    private readonly ReturningSpecimenFiltersBuilder returningSpecimenFiltersBuilder = new();
    private readonly SpecimensProcessorBuilder specimensProcessorBuilder = new();
    private readonly StatisticsProcessorBuilder statisticsProcessorBuilder = new();
    private readonly SummaryProcessorBuilder summaryProcessorBuilder = new();
    private readonly SummaryWriterBuilder summaryWriterBuilder = new();

    public void Run(ApplicationConfig appConfig)
    {
        ArgumentNullException.ThrowIfNull(appConfig);

        var database = databaseBuilder
            .WithDatabaseParameters(appConfig.Databases)
            .Build();

        var observationFilters = observationFiltersBuilder
            .WithObservationConditions(appConfig.Observations.Conditions)
            .Build();

        var returningSpecimenFilters = returningSpecimenFiltersBuilder
            .WithReturningSpecimensConditions(appConfig.ReturningSpecimens.Conditions)
            .Build();

        var populationProcessor = populationProcessorBuilder
            .WithPopulationConditions(appConfig.Population.Conditions)
            .Build();

        var statisticsProcessor = statisticsProcessorBuilder
            .WithStatisticsOperations(appConfig.Statistics.Operations)
            .Build();

        var specimensProcessor = specimensProcessorBuilder
            .WithSpecimensParameters(appConfig.Specimens)
            .Build();

        var resultsWriter = summaryWriterBuilder
            .WithWriterParameters(appConfig.Results.Writer)
            .Build();

        var observations = database.GetObservations().ToArray();

        Log.Information($"Odczytano {observations.Length} obserwacji.");

        var selectedObservations = observations.Where(observationFilters.IsAccepted).ToArray();

        Log.Information($"Wybrano {selectedObservations.Length} obserwacji.");

        var specimens = specimensProcessor.GetSpecimens(selectedObservations).ToArray();

        Log.Information($"Wybrano {specimens.Length} osobników.");

        var returningSpecimens = specimens.Where(returningSpecimenFilters.IsReturning).ToArray();

        Log.Information($"Wybrano {returningSpecimens.Length} powracających osobników.");

        var summaryProcessor = summaryProcessorBuilder
            .WithPopulationProcessor(populationProcessor)
            .WithStatisticsProcessor(statisticsProcessor)
            .WithTotalPopulation(specimens)
            .Build();

        var summary = returningSpecimens
            .Select(summaryProcessor.GetSummary)
            .ToList();

        Log.Information($"Przygotowano {summary.Count} wierszy danych wynikowych.");

        resultsWriter.Write(summary);

        Log.Information($"Zapisano dane wynikowe do pliku {appConfig.Results.Writer.Path}.");
    }
}