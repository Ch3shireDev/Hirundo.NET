using Hirundo.Commons;
using Hirundo.Commons.Helpers;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription("IsGreaterOrEqual")]
public class IsGreaterOrEqualReturningCondition : CompareValuesReturningCondition
{
    public IsGreaterOrEqualReturningCondition(string valueName, object? value) : base(valueName, value) { }
    public IsGreaterOrEqualReturningCondition() : base() { }
    public override bool Compare(object? observationValue, object? value) => ComparisonHelpers.IsGreaterOrEqual(observationValue, value);
}
