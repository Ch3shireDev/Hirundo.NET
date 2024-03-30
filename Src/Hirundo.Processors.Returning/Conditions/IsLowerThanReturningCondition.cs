using Hirundo.Commons;
using Hirundo.Commons.Helpers;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription("IsLowerThan")]
public class IsLowerThanReturningCondition : CompareValuesReturningCondition
{
    public IsLowerThanReturningCondition(string valueName, object? value) : base(valueName, value) { }
    public IsLowerThanReturningCondition() : base() { }
    public override bool Compare(object? observationValue, object? value) => ComparisonHelpers.IsLower(observationValue, value);
}
