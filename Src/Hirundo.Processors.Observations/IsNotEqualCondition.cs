using Hirundo.Commons;

namespace Hirundo.Processors.Observations;

[TypeDescription("IsNotEqual")]
public class IsNotEqualCondition : IObservationCondition
{
    public IsNotEqualCondition() { }

    public IsNotEqualCondition(string valueName, object? value)
    {
        ValueName = valueName;
        Value = value;
    }

    public string ValueName { get; set; } = null!;
    public object? Value { get; set; }

    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        var observationValue = observation.GetValue(ValueName);
        return !DataTypeHelpers.SoftEquals(Value, observationValue);
    }
}