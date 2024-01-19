namespace Hirundo.Processors.Observations.Conditions;

/// <summary>
///     Budowniczy filtrów obserwacji. Filtry mogą być budowane na podstawie danych wprowadzonych przez użytkownika,
///     tworząc filtry składające się z kilku warunków z operatorem logicznym AND.
/// </summary>
public class ObservationFiltersBuilder
{
    private readonly List<IObservationCondition> _observationFilters = [];

    /// <summary>
    ///     Tworzy nowy filtr obserwacji.
    /// </summary>
    /// <returns></returns>
    public IObservationCondition Build()
    {
        return new CompositeObservationCondition([.._observationFilters]);
    }

    public ObservationFiltersBuilder WithObservationConditions(IEnumerable<IObservationCondition> observationsConditions)
    {
        _observationFilters.AddRange(observationsConditions);
        return this;
    }
}