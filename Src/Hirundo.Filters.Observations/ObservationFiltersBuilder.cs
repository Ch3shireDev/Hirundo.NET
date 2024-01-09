namespace Hirundo.Filters.Observations;

/// <summary>
///     Budowniczy filtrów obserwacji. Filtry mogą być budowane na podstawie danych wprowadzonych przez użytkownika,
///     tworząc filtry składające się z kilku warunków z operatorem logicznym AND.
/// </summary>
public class ObservationFiltersBuilder
{
    private readonly List<IObservationFilter> _observationFilters = [];

    /// <summary>
    ///     Tworzy nowy filtr obserwacji.
    /// </summary>
    /// <returns></returns>
    public IObservationFilter Build()
    {
        return new CompositeObservationFilter([.._observationFilters]);
    }

    public ObservationFiltersBuilder WithConditions(IEnumerable<IObservationFilter> observationsConditions)
    {
        _observationFilters.AddRange(observationsConditions);
        return this;
    }
}