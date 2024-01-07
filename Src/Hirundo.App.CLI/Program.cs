using Hirundo.Databases;
using Hirundo.Filters.Observations;
using Hirundo.Filters.Specimens;
using Hirundo.Processors.Population;
using Hirundo.Processors.Specimens;
using Hirundo.Processors.Statistics;
using Hirundo.Processors.Summary;
using Hirundo.Writers.Summary;

namespace Hirundo.App.CLI;

/// <summary>
///     Przykładowa aplikacja konsolowa, która wykorzystuje bibliotekę Hirundo. Program pobiera dane z dwóch tabel w bazie
///     danych Access, przetwarza je i zapisuje wyniki do pliku CSV.
/// </summary>
internal class Program
{
    private static void Main()
    {
        var oldDatabaseParameters = GetOldAccessDatabaseParameters();
        var newDatabaseParameters = GetNewAccessDatabaseParameters();

        var specimensProcessorParameters = new SpecimensProcessorParameters
        {
            RingValueName = "RING"
        };

        var csvSummaryWriterParameters = new CsvSummaryWriterParameters
        {
            SummaryFilepath = @"summary.csv"
        };

        var databaseBuilder = new DatabaseBuilder();
        var specimensProcessorBuilder = new SpecimensProcessorBuilder();
        var observationFiltersBuilder = new ObservationFiltersBuilder();
        var returningSpecimenFiltersBuilder = new ReturningSpecimenFiltersBuilder();
        var populationProcessorBuilder = new PopulationProcessorBuilder();
        var statisticsProcessorBuilder = new StatisticsProcessorBuilder();
        var summaryProcessorBuilder = new SummaryProcessorBuilder();
        var summaryWriterBuilder = new SummaryWriterBuilder();

        var compositeDatabase = databaseBuilder
            .AddMdbAccessDatabase(oldDatabaseParameters)
            .AddMdbAccessDatabase(newDatabaseParameters)
            .Build();

        var observationFilters = observationFiltersBuilder
            .Build();

        var returningSpecimenFilters = returningSpecimenFiltersBuilder
            .Build();

        var populationProcessor = populationProcessorBuilder
            .Build();

        var statisticsProcessor = statisticsProcessorBuilder
            .Build();

        var specimensProcessor = specimensProcessorBuilder
            .WithSpecimensProcessorParameters(specimensProcessorParameters)
            .Build();

        var resultsWriter = summaryWriterBuilder
            .WithCsvSummaryWriterParameters(csvSummaryWriterParameters)
            .Build();

        var observations = compositeDatabase.GetObservations().ToArray();
        var selectedObservations = observations.Where(observationFilters.IsAccepted);
        var specimens = specimensProcessor.GetSpecimens(selectedObservations).ToArray();
        var returningSpecimens = specimens.Where(returningSpecimenFilters.IsReturning);

        var summaryProcessor = summaryProcessorBuilder
            .WithPopulationProcessor(populationProcessor)
            .WithStatisticsProcessor(statisticsProcessor)
            .WithTotalPopulation(specimens)
            .Build();

        var summary = returningSpecimens
            .Select(summaryProcessor.GetSummary)
            .ToList();

        resultsWriter.WriteSummary(summary);
    }

    private static AccessDatabaseParameters GetOldAccessDatabaseParameters()
    {
        var oldDatabaseParameters = new AccessDatabaseParameters
        {
            FilePath = @"D:\Ring_00_PODAB.mdb",
            TableName = "TAB_RING_PODAB",
            ValuesColumns =
            {
                new DatabaseColumn("IDR_Podab", "ID", DataValueType.LongInt),
                new DatabaseColumn("RING", "RING", DataValueType.String),
                new DatabaseColumn("SPEC", "SPECIES", DataValueType.String),
                new DatabaseColumn("DATE", "DATE", DataValueType.DateTime),
                new DatabaseColumn("HOUR", "HOUR", DataValueType.ShortInt),
                new DatabaseColumn("SEX", "SEX", DataValueType.String),
                new DatabaseColumn("AGE", "AGE", DataValueType.String),
                new DatabaseColumn("MASS", "WEIGHT", DataValueType.Decimal),
                new DatabaseColumn("WING", "WING", DataValueType.Decimal),
                new DatabaseColumn("TAIL", "TAIL", DataValueType.Decimal),
                new DatabaseColumn("FAT", "FAT", DataValueType.ShortInt),
                new DatabaseColumn("D2", "D2", DataValueType.ShortInt),
                new DatabaseColumn("D3", "D3", DataValueType.ShortInt),
                new DatabaseColumn("D4", "D4", DataValueType.ShortInt),
                new DatabaseColumn("D5", "D5", DataValueType.ShortInt),
                new DatabaseColumn("D6", "D6", DataValueType.ShortInt),
                new DatabaseColumn("D7", "D7", DataValueType.ShortInt),
                new DatabaseColumn("D8", "D8", DataValueType.ShortInt)
            }
        };
        return oldDatabaseParameters;
    }
    private static AccessDatabaseParameters GetNewAccessDatabaseParameters()
    {
        var newDatabaseParameters = new AccessDatabaseParameters
        {
            FilePath = @"D:\Ring_00_PODAB.mdb",
            TableName = "AB 2017_18_19_20_21S",
            ValuesColumns =
            {
                new DatabaseColumn("IDR_Podab", "ID", DataValueType.LongInt),
                new DatabaseColumn("RING", "RING", DataValueType.String),
                new DatabaseColumn("Species Code", "SPECIES", DataValueType.String),
                new DatabaseColumn("Date2", "DATE", DataValueType.DateTime),
                new DatabaseColumn("HOUR", "HOUR", DataValueType.ShortInt),
                new DatabaseColumn("SEX", "SEX", DataValueType.String),
                new DatabaseColumn("AGE", "AGE", DataValueType.String),
                new DatabaseColumn("WEIGHT", "WEIGHT", DataValueType.Decimal),
                new DatabaseColumn("WING", "WING", DataValueType.Decimal),
                new DatabaseColumn("TAIL", "TAIL", DataValueType.Decimal),
                new DatabaseColumn("FAT", "FAT", DataValueType.ShortInt),
                new DatabaseColumn("D2", "D2", DataValueType.ShortInt),
                new DatabaseColumn("D3", "D3", DataValueType.ShortInt),
                new DatabaseColumn("D4", "D4", DataValueType.ShortInt),
                new DatabaseColumn("D5", "D5", DataValueType.ShortInt),
                new DatabaseColumn("D6", "D6", DataValueType.ShortInt),
                new DatabaseColumn("D7", "D7", DataValueType.ShortInt),
                new DatabaseColumn("D8", "D8", DataValueType.ShortInt)
            }
        };
        return newDatabaseParameters;
    }
}