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
            Path = @"D:\Ring_00_PODAB.mdb",
            Table = "TAB_RING_PODAB",
            Columns =
            {
                new ColumnMapping("IDR_Podab", "ID", DataValueType.LongInt),
                new ColumnMapping("RING", "RING", DataValueType.String),
                new ColumnMapping("SPEC", "SPECIES", DataValueType.String),
                new ColumnMapping("DATE", "DATE", DataValueType.DateTime),
                new ColumnMapping("HOUR", "HOUR", DataValueType.ShortInt),
                new ColumnMapping("SEX", "SEX", DataValueType.String),
                new ColumnMapping("AGE", "AGE", DataValueType.String),
                new ColumnMapping("MASS", "WEIGHT", DataValueType.Decimal),
                new ColumnMapping("WING", "WING", DataValueType.Decimal),
                new ColumnMapping("TAIL", "TAIL", DataValueType.Decimal),
                new ColumnMapping("FAT", "FAT", DataValueType.ShortInt),
                new ColumnMapping("D2", "D2", DataValueType.ShortInt),
                new ColumnMapping("D3", "D3", DataValueType.ShortInt),
                new ColumnMapping("D4", "D4", DataValueType.ShortInt),
                new ColumnMapping("D5", "D5", DataValueType.ShortInt),
                new ColumnMapping("D6", "D6", DataValueType.ShortInt),
                new ColumnMapping("D7", "D7", DataValueType.ShortInt),
                new ColumnMapping("D8", "D8", DataValueType.ShortInt)
            }
        };
        return oldDatabaseParameters;
    }
    private static AccessDatabaseParameters GetNewAccessDatabaseParameters()
    {
        var newDatabaseParameters = new AccessDatabaseParameters
        {
            Path = @"D:\Ring_00_PODAB.mdb",
            Table = "AB 2017_18_19_20_21S",
            Columns =
            {
                new ColumnMapping("IDR_Podab", "ID", DataValueType.LongInt),
                new ColumnMapping("RING", "RING", DataValueType.String),
                new ColumnMapping("Species Code", "SPECIES", DataValueType.String),
                new ColumnMapping("Date2", "DATE", DataValueType.DateTime),
                new ColumnMapping("HOUR", "HOUR", DataValueType.ShortInt),
                new ColumnMapping("SEX", "SEX", DataValueType.String),
                new ColumnMapping("AGE", "AGE", DataValueType.String),
                new ColumnMapping("WEIGHT", "WEIGHT", DataValueType.Decimal),
                new ColumnMapping("WING", "WING", DataValueType.Decimal),
                new ColumnMapping("TAIL", "TAIL", DataValueType.Decimal),
                new ColumnMapping("FAT", "FAT", DataValueType.ShortInt),
                new ColumnMapping("D2", "D2", DataValueType.ShortInt),
                new ColumnMapping("D3", "D3", DataValueType.ShortInt),
                new ColumnMapping("D4", "D4", DataValueType.ShortInt),
                new ColumnMapping("D5", "D5", DataValueType.ShortInt),
                new ColumnMapping("D6", "D6", DataValueType.ShortInt),
                new ColumnMapping("D7", "D7", DataValueType.ShortInt),
                new ColumnMapping("D8", "D8", DataValueType.ShortInt)
            }
        };
        return newDatabaseParameters;
    }
}