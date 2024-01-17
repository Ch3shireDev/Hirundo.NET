using Hirundo.Commons;

namespace Hirundo.Processors.Observations.Conditions;

[TypeDescription("IsEqual")]
public class IsEqualFilter : IObservationFilter
{
    public IsEqualFilter()
    {
        ValueName = null!;
        Value = null!;
    }

    public IsEqualFilter(string valueName, object value)
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