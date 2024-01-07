using Hirundo.Commons;

namespace Hirundo.Filters.Observations;

public class CompositeObservationFilter(params IObservationFilter[] filters) : IObservationFilter
{
    public bool IsSelected(Observation observation)
    {
        return filters.All(f => f.IsSelected(observation));
    }
}