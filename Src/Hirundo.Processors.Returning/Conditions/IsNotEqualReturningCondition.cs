using Hirundo.Commons;
using Hirundo.Commons.Helpers;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription(
    "IsNotEqual",
    "Czy dane nie są równe?",
    "Osobnik zawiera obserwację z polem różnym od danej wartości.")]
public class IsNotEqualReturningCondition : CompareValuesReturningCondition
{
    public IsNotEqualReturningCondition(string valueName, object? value) : base(valueName, value) { }
    public IsNotEqualReturningCondition() : base() { }
    public override bool Compare(object? observationValue, object? value)
    {
        return observationValue != null && !ComparisonHelpers.IsEqual(observationValue, value);
    }
}
