using Hirundo.Commons;

namespace Hirundo.Processors.Observations.Conditions;

[TypeDescription("IsEqual")]
public class IsEqualCondition : IObservationCondition
{
    public IsEqualCondition()
    {
        ValueName = null!;
        Value = null!;
    }

    public IsEqualCondition(string valueName, object value)
    {
        ValueName = valueName;
        Value = value;
    }

    public string ValueName { get; set; }
    public object Value { get; set; }

    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        var observationValue = observation.GetValue(ValueName);
        return Equals(Value, observationValue);
    }
}