using Hirundo.Commons;

namespace Hirundo.Processors.Observations.Conditions;

[TypeDescription("IsNotEqual")]
public class IsNotEqualCondition(string valueName, object value) : IObservationCondition
{
    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);

        return observation.GetValue(valueName) != value;
    }
}