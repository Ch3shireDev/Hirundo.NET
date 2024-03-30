using Hirundo.Commons;
using Hirundo.Commons.Helpers;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription("IsEqual")]
public class IsEqualReturningCondition : CompareValuesReturningCondition
{
    public IsEqualReturningCondition(string valueName, object? value) : base(valueName, value) { }
    public IsEqualReturningCondition() : base() { }
    public override bool Compare(object? observationValue, object? value) => ComparisonHelpers.IsEqual(observationValue, value);
}
