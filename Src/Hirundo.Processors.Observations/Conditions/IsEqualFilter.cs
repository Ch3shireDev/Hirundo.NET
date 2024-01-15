using Hirundo.Commons;

namespace Hirundo.Processors.Observations.Conditions;

[TypeDescription("IsEqual")]
public class IsEqualFilter(string valueName, object value) : IObservationFilter
{
    public string ValueName { get; } = valueName;
    public object Value { get; } = value;

    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        var observationValue = observation.GetValue(ValueName);
        return Equals(Value, observationValue);
    }
}