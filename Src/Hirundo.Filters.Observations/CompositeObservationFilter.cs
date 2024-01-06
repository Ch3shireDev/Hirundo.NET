using Hirundo.Commons;

namespace Hirundo.Filters.Observations;

public class CompositeObservationFilter : IObservationFilter
{
    public bool IsSelected(Observation observation)
    {
        return true;
    }
}