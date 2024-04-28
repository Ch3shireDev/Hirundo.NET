using Hirundo.Commons;
using Hirundo.Commons.Helpers;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription(
    "IsNotEqual",
    "Czy dane nie są równe?",
    "Osobnik zawiera obserwację z polem różnym od danej wartości.")]
public class IsNotEqualReturningCondition : CompareValuesReturningCondition, ISelfExplainer
{
    public IsNotEqualReturningCondition(string valueName, object? value) : base(valueName, value)
    {
    }

    public IsNotEqualReturningCondition()
    {
    }

    public string Explain()
    {
        return $"Którakolwiek z obserwacji osobnika musi mieć wartość {ValueName} różną od {Value}.";
    }

    public override bool Compare(object? observationValue, object? value)
    {
        return observationValue != null && !ComparisonHelpers.IsEqual(observationValue, value);
    }
}