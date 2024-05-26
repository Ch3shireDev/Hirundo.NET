using Hirundo.Commons;
using Hirundo.Commons.Helpers;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription(
    "IsLowerThan",
    "Czy dane są mniejsze?",
    "Osobnik zawiera obserwację z polem mniejszym od danej wartości."
)]
public class IsLowerThanReturningCondition : CompareValuesReturningCondition, ISelfExplainer
{
    public IsLowerThanReturningCondition(string valueName, object? value) : base(valueName, value)
    {
    }

    public IsLowerThanReturningCondition()
    {
    }

    public string Explain()
    {
        return $"Którakolwiek z obserwacji osobnika musi mieć wartość {ValueName} mniejszą niż {Value}.";
    }

    public override bool Compare(object? observationValue, object? value)
    {
        return ComparisonHelpers.IsLower(observationValue, value);
    }
}