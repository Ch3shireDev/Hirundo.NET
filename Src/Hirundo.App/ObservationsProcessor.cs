using Hirundo.Commons.Models;
using Hirundo.Databases;
using Hirundo.Processors.Computed;
using Hirundo.Processors.Observations;
using Hirundo.Serialization.Json;
using Serilog;

namespace Hirundo.App;

public class ObservationsProcessor(IDatabaseBuilder databaseBuilder, IComputedValuesCalculatorBuilder computedValuesCalculatorBuilder, IObservationConditionsBuilder observationConditionsBuilder)
{
    public bool RequireNonEmptyRing { get; set; }

    public ObservationsProcessor() : this(new DatabaseBuilder(), new ComputedValuesCalculatorBuilder(), new ObservationConditionsBuilder())
    {

    }

    private readonly IDatabaseBuilder _databaseBuilder = databaseBuilder;
    private readonly IObservationConditionsBuilder _observationConditionsBuilder = observationConditionsBuilder;
    private readonly IComputedValuesCalculatorBuilder _calculatorBuilder = computedValuesCalculatorBuilder;

    public Observation[] GetObservations(ApplicationParameters applicationConfig, CancellationToken? token = null)
    {
        Observation[] observations1 = GetObservationsFromDataSource(applicationConfig, token);

        Log.Information($"Odczytano {observations1.Length} obserwacji. Wyliczanie dodatkowych wartości...");

        Observation[] observations2 = CalculateValues(applicationConfig, token, observations1);

        Log.Information("Filtrowanie obserwacji po warunkach...");

        Observation[] observations3 = FilterObservations(applicationConfig, token, observations2);

        return observations3;
    }

    private Observation[] FilterObservations(ApplicationParameters applicationConfig, CancellationToken? token, Observation[] observations2)
    {
        var observationConditions = _observationConditionsBuilder
            .NewBuilder()
            .WithObservationConditions(applicationConfig.Observations.Conditions)
            .WithCancellationToken(token)
            .Build();

        Observation[] observations3 = [.. observationConditions.Filter(observations2)];
        return observations3;
    }

    private Observation[] CalculateValues(ApplicationParameters applicationConfig, CancellationToken? token, Observation[] observations)
    {
        var computedValuesCalculator = _calculatorBuilder
            .NewBuilder()
            .WithComputedValues(applicationConfig.ComputedValues.ComputedValues)
            .WithCancellationToken(token)
            .Build();

        Observation[] observations2 = [.. computedValuesCalculator.Calculate(observations)];
        return observations2;
    }

    private IList<Observation> _dataSourceCache = [];
    private string _dataSourceHash = "";

    private Observation[] GetObservationsFromDataSource(ApplicationParameters applicationConfig, CancellationToken? token)
    {
        var dataSourceHash = JsonTools.GetConfigHash(applicationConfig.Databases);

        if (_dataSourceCache.Any() && _dataSourceHash.Equals(dataSourceHash))
        {
            Log.Information("Korzystanie z załadowanych wcześniej wartości z bazy danych.");
            return [.. _dataSourceCache.Select(x => x.Copy())];
        }

        var dataSource = _databaseBuilder
            .WithDatabaseParameters(applicationConfig.Databases.Databases)
            .WithCancellationToken(token)
            .Build();

        var observations = dataSource.GetObservations();

        if (RequireNonEmptyRing)
        {
            observations = observations.Where(o => !string.IsNullOrWhiteSpace(o.Ring)).ToArray();
        }

        _dataSourceCache = [.. observations.Select(o => o.Copy())];
        _dataSourceHash = dataSourceHash;

        return [.. observations];
    }
}
