using Serilog;

namespace Hirundo.Processors.Observations;

/// <summary>
///     Budowniczy filtrów obserwacji. Filtry mogą być budowane na podstawie danych wprowadzonych przez użytkownika,
///     tworząc filtry składające się z kilku warunków z operatorem logicznym AND.
/// </summary>
public class ObservationConditionsBuilder : IObservationConditionsBuilder
{
    private readonly List<IObservationCondition> _observationConditions = [];
    private CancellationToken? _cancellationToken;

    /// <summary>
    ///     Tworzy nowy filtr obserwacji.
    /// </summary>
    /// <returns></returns>
    public IObservationCondition Build()
    {
        Log.Information("Budowanie warunków obserwacji. Liczba warunków: {_observationConditionsCount}.", _observationConditions.Count);
        return new CompositeObservationCondition([.. _observationConditions], _cancellationToken);
    }

    public IObservationConditionsBuilder NewBuilder()
    {
        return new ObservationConditionsBuilder();
    }

    public IObservationConditionsBuilder WithCancellationToken(CancellationToken? cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return this;
    }

    public IObservationConditionsBuilder WithObservationConditions(IEnumerable<IObservationCondition> observationsConditions)
    {
        _observationConditions.AddRange(observationsConditions);
        return this;
    }
}