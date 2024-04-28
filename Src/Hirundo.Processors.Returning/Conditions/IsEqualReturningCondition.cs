using Hirundo.Commons;
using Hirundo.Commons.Helpers;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription(
    "IsEqual",
    "Czy dane są równe?",
    "Osobnik zawiera obserwację z polem równym danej wartości."
)]
public class IsEqualReturningCondition : CompareValuesReturningCondition
{
    public IsEqualReturningCondition(string valueName, object? value) : base(valueName, value)
    {
    }

    public IsEqualReturningCondition()
    {
    }

    public override bool Compare(object? observationValue, object? value)
    {
        return ComparisonHelpers.IsEqual(observationValue, value);
    }
}