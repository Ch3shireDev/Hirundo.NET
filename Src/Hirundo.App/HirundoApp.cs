using Hirundo.Databases;
using Hirundo.Processors.Computed;
using Hirundo.Processors.Observations.Conditions;
using Hirundo.Processors.Population;
using Hirundo.Processors.Returning;
using Hirundo.Processors.Specimens;
using Hirundo.Processors.Statistics;
using Hirundo.Processors.Summary;
using Hirundo.Writers.Summary;
using Serilog;

namespace Hirundo.App;

public class HirundoApp : IHirundoApp
{
    private readonly IComputedValuesCalculatorBuilder _calculatorBuilder = new ComputedValuesCalculatorBuilder();
    private readonly IDatabaseBuilder _databaseBuilder = new DatabaseBuilder();
    private readonly IObservationConditionsBuilder _observationConditionsBuilder = new ObservationConditionsBuilder();
    private readonly IPopulationProcessorBuilder _populationProcessorBuilder = new PopulationProcessorBuilder();
    private readonly IReturningSpecimenConditionsBuilder _returningSpecimenConditionsBuilder = new ReturningSpecimenConditionsBuilder();
    private readonly ISpecimensProcessorBuilder _specimensProcessorBuilder = new SpecimensProcessorBuilder();
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
        ISpecimensProcessorBuilder specimensProcessorBuilder,
        IStatisticsProcessorBuilder statisticsProcessorBuilder,
        ISummaryWriterBuilder summaryWriterBuilder
    )
    {
        _databaseBuilder = databaseBuilder;
        _calculatorBuilder = calculatorBuilder;
        _observationConditionsBuilder = observationsBuilder;
        _returningSpecimenConditionsBuilder = returningBuilder;
        _populationProcessorBuilder = populationProcessorBuilder;
        _specimensProcessorBuilder = specimensProcessorBuilder;
        _statisticsProcessorBuilder = statisticsProcessorBuilder;
        _summaryWriterBuilder = summaryWriterBuilder;
    }

    public void Run(ApplicationConfig applicationConfig, CancellationToken? token = null)
    {
        ArgumentNullException.ThrowIfNull(applicationConfig);

        var database = _databaseBuilder
            .WithDatabaseParameters([.. applicationConfig.Databases])
            .WithCancellationToken(token)
            .Build();

        var observationConditions = _observationConditionsBuilder
            .WithObservationConditions(applicationConfig.Observations.Conditions)
            .Build();

        var computedValuesCalculator = _calculatorBuilder
            .WithComputedValues(applicationConfig.ComputedValues.ComputedValues)
            .Build();

        var returningSpecimenConditions = _returningSpecimenConditionsBuilder
            .WithReturningSpecimensConditions(applicationConfig.ReturningSpecimens.Conditions)
            .Build();

        var populationProcessor = _populationProcessorBuilder
            .WithPopulationConditions(applicationConfig.Population.Conditions)
            .Build();

        var specimensProcessor = _specimensProcessorBuilder
            .WithSpecimensParameters(applicationConfig.Specimens)
            .Build();

        var statisticsProcessor = _statisticsProcessorBuilder
            .WithStatisticsOperations(applicationConfig.Statistics.Operations)
            .Build();

        using var resultsWriter = _summaryWriterBuilder
            .WithWriterParameters(applicationConfig.Results.Writer)
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

        var specimens = specimensProcessor.GetSpecimens(selectedObservations).ToArray();

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

        Log.Information($"Przygotowano {summary.Count} wierszy danych wynikowych.");

        resultsWriter.Write(summary);

        Log.Information($"Zapisano dane wynikowe do pliku {applicationConfig.Results.Writer.Path}.");
    }
}