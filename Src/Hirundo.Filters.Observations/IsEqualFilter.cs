using Hirundo.Commons;

namespace Hirundo.Filters.Observations;

[TypeDescription("IsEqual")]
public class IsEqualFilter(string valueName, object value) : IObservationFilter
{
    public string ValueName { get; } = valueName;
    public object Value { get; } = value;

    public bool IsAccepted(Observation observation)
    {
        var observationValue = observation.GetValue(ValueName);
        return Equals(Value, observationValue);
    }
}