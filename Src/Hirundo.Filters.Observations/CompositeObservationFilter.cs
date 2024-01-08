using Hirundo.Commons;

namespace Hirundo.Filters.Observations;

/// <summary>
///     Klasa reprezentująca złożenie wielu warunków na obserwację. Warunki są łączone operatorem logicznym AND,
///     tj. obserwacja jest wybierana tylko jeśli spełnia wszystkie warunki.
/// </summary>
/// <param name="filters"></param>
public class CompositeObservationFilter(params IObservationFilter[] filters) : IObservationFilter
{
    public bool IsAccepted(Observation observation)
    {
        return filters.All(f => f.IsAccepted(observation));
    }
}