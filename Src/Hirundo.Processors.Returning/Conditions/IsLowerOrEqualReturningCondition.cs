using Hirundo.Commons;
using Hirundo.Commons.Helpers;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription(
    "IsLowerOrEqual",
    "Czy dane są mniejsze lub równe?",
    "Osobnik zawiera obserwację z polem mniejszym lub równym danej wartości."
)]
public class IsLowerOrEqualReturningCondition : CompareValuesReturningCondition
{
    public IsLowerOrEqualReturningCondition(string valueName, object? value) : base(valueName, value)
    {
    }

    public IsLowerOrEqualReturningCondition()
    {
    }

    public override bool Compare(object? observationValue, object? value)
    {
        return ComparisonHelpers.IsLowerOrEqual(observationValue, value);
    }
}