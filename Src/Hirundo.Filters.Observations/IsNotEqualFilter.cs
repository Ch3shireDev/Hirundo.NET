using Hirundo.Commons;

namespace Hirundo.Filters.Observations;

[TypeDescription("IsNotEqual")]
public class IsNotEqualFilter(string valueName, object value) : IObservationFilter
{
    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);

        return observation.GetValue(valueName) != value;
    }
}