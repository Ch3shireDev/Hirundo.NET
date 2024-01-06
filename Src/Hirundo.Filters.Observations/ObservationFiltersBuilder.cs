namespace Hirundo.Filters.Observations;

/// <summary>
///     Budowniczy filtrów obserwacji. Filtry mogą być budowane na podstawie danych wprowadzonych przez użytkownika,
///     tworząc filtry składające się z kilku warunków z operatorem logicznym AND.
/// </summary>
public class ObservationFiltersBuilder
{
    /// <summary>
    ///     Tworzy nowy filtr obserwacji.
    /// </summary>
    /// <returns></returns>
    public IObservationFilter Build()
    {
        return new CompositeObservationFilter();
    }
}