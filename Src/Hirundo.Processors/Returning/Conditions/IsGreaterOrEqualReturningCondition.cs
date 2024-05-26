using Hirundo.Commons;
using Hirundo.Commons.Helpers;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription(
    "IsGreaterOrEqual",
    "Czy dane są większe lub równe?",
    "Osobnik zawiera obserwację z polem większym lub równym danej wartości."
)]
public class IsGreaterOrEqualReturningCondition : CompareValuesReturningCondition, ISelfExplainer
{
    public IsGreaterOrEqualReturningCondition(string valueName, object? value) : base(valueName, value)
    {
    }

    public IsGreaterOrEqualReturningCondition()
    {
    }

    public string Explain()
    {
        return $"Którakolwiek z obserwacji osobnika musi mieć wartość {ValueName} większą lub równą {Value}.";
    }

    public override bool Compare(object? observationValue, object? value)
    {
        return ComparisonHelpers.IsGreaterOrEqual(observationValue, value);
    }
}