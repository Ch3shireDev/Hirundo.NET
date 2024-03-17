using Hirundo.Commons;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription("IsGreaterThan")]
public class IsGreaterThanReturningCondition : CompareValuesReturningCondition
{
    public IsGreaterThanReturningCondition(string valueName, object? value) : base(valueName, value) { }
    public IsGreaterThanReturningCondition() : base() { }
    public override bool Compare(object? observationValue, object? value) => ComparisonHelpers.IsGreater(observationValue, value);
}