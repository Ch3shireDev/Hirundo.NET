using Hirundo.Commons;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription("IsLowerOrEqual")]
public class IsLowerOrEqualReturningCondition : CompareValuesReturningCondition
{
    public IsLowerOrEqualReturningCondition(string valueName, object? value) : base(valueName, value) { }
    public IsLowerOrEqualReturningCondition() : base() { }
    public override bool Compare(object? observationValue, object? value) => ComparisonHelpers.IsLowerOrEqual(observationValue, value);
}
