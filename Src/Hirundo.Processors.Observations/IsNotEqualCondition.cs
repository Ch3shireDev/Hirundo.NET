using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

[TypeDescription("IsNotEqual")]
public class IsNotEqualCondition : IObservationCondition, ICompareValueCondition
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
        return !ComparisonHelpers.IsEqual(Value, observationValue);
    }
}