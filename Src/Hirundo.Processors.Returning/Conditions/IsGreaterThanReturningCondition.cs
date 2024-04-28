using Hirundo.Commons;
using Hirundo.Commons.Helpers;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription(
    "IsGreaterThan",
    "Czy dane są większe?",
    "Osobnik zawiera obserwację z polem większym od danej wartości."
)]
public class IsGreaterThanReturningCondition : CompareValuesReturningCondition, ISelfExplainer
{
    public IsGreaterThanReturningCondition(string valueName, object? value) : base(valueName, value)
    {
    }

    public IsGreaterThanReturningCondition()
    {
    }

    public string Explain()
    {
        return $"Którakolwiek z obserwacji osobnika musi mieć wartość {ValueName} większą niż {Value}.";
    }

    public override bool Compare(object? observationValue, object? value)
    {
        return ComparisonHelpers.IsGreater(observationValue, value);
    }
}