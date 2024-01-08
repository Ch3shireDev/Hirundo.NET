using Hirundo.Commons;

namespace Hirundo.Filters.Observations;

public class IsEqualValueFilter(string valueName, object value) : IObservationFilter
{
    public string ValueName { get; } = valueName;
    public object Value { get; } = value;

    public bool IsAccepted(Observation observation)
    {
        return observation.GetValue(ValueName) == Value;
    }
}