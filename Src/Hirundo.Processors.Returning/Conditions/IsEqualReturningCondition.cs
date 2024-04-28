using Hirundo.Commons;
using Hirundo.Commons.Helpers;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription(
    "IsEqual",
    "Czy dane są równe?",
    "Osobnik zawiera obserwację z polem równym danej wartości."
)]
public class IsEqualReturningCondition : CompareValuesReturningCondition, ISelfExplainer
{
    public IsEqualReturningCondition(string valueName, object? value) : base(valueName, value)
    {
    }

    public IsEqualReturningCondition()
    {
    }

    public string Explain()
    {
        return $"Którakolwiek z obserwacji osobnika musi mieć wartość {ValueName} równą {Value}.";
    }

    public override bool Compare(object? observationValue, object? value)
    {
        return ComparisonHelpers.IsEqual(observationValue, value);
    }
}