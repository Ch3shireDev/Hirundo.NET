using Hirundo.Commons;

namespace Hirundo.Filters.Observations;

public class SkipValueFilter(string valueName, object value) : IObservationFilter
{
    public bool IsSelected(Observation observation)
    {
        return observation.GetValue(valueName) != value;
    }
}
