using Hirundo.Configuration;
using Hirundo.Databases;
using Hirundo.Filters.Observations;
using Hirundo.Filters.Specimens;
using Hirundo.Processors.Population;
using Hirundo.Processors.Specimens;
using Hirundo.Processors.Statistics;
using Hirundo.Processors.Summary;
using Hirundo.Serialization.Json;
using Hirundo.Writers.Summary;
using Newtonsoft.Json;
using Serilog;
using System.Globalization;
using System.Runtime.Serialization;

namespace Hirundo.App.CLI;

/// <summary>
///     Przykładowa aplikacja konsolowa, która wykorzystuje bibliotekę Hirundo. 
///     Program pobiera dane z dwóch tabel w bazie danych Access, przetwarza je i zapisuje wyniki do pliku CSV.
/// </summary>
internal sealed class Program
{
    private static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(formatProvider:CultureInfo.InvariantCulture)
            .CreateLogger();

        var appConfig = GetConfig();

        var databaseBuilder = new DatabaseBuilder();
        var specimensProcessorBuilder = new SpecimensProcessorBuilder();
        var observationFiltersBuilder = new ObservationFiltersBuilder();
        var returningSpecimenFiltersBuilder = new ReturningSpecimenFiltersBuilder();
        var populationProcessorBuilder = new PopulationProcessorBuilder();
        var statisticsProcessorBuilder = new StatisticsProcessorBuilder();
        var summaryProcessorBuilder = new SummaryProcessorBuilder();
        var summaryWriterBuilder = new SummaryWriterBuilder();

        var database = databaseBuilder
            .AddDatabaseParameters(appConfig.Databases)
            .Build();

        var observationFilters = observationFiltersBuilder
            .WithConditions(appConfig.Observations.Conditions)
            .Build();

        var returningSpecimenFilters = returningSpecimenFiltersBuilder
            .WithConditions(appConfig.ReturningSpecimens.Conditions)
            .Build();

        var populationProcessor = populationProcessorBuilder
            .WithConditions(appConfig.Population.Conditions)
            .Build();

        var statisticsProcessor = statisticsProcessorBuilder
            .WithOperations(appConfig.Statistics.Operations)
            .WithOutliersConditions(appConfig.Statistics.Outliers.Conditions)
            .Build();

        var specimensProcessor = specimensProcessorBuilder
            .WithSpecimensProcessorParameters(appConfig.Specimens)
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
    }

    private static ApplicationConfig GetConfig()
    {
        var converter = new HirundoJsonConverter();
        var jsonConfig = File.ReadAllText("appsettings.json");
        var appConfig = JsonConvert.DeserializeObject<ApplicationConfig>(jsonConfig, converter) ?? throw new SerializationException("Błąd parsowania konfiguracji.");
        return appConfig;
    }
}