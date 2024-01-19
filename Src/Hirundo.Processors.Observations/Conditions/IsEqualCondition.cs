using Hirundo.Commons;

namespace Hirundo.Processors.Observations.Conditions;

[TypeDescription("IsEqual")]
public class IsEqualCondition(string valueName, object value) : IObservationCondition
{
    public IsEqualCondition() : this(null!, null!)
    {
    }

    public string ValueName { get; set; } = valueName;
    public object Value { get; set; } = value;

    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        var observationValue = observation.GetValue(ValueName);
        return Equals(Value, observationValue);
    }
}