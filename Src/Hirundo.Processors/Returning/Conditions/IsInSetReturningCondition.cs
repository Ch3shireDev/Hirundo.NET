using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription("IsInSet",
    "Czy dane powracającego osobnika są w zbiorze?",
    "Warunek sprawdzający, czy pole danych dla osobnika powracającego znajduje się w zbiorze wartości.")]
public class IsInSetReturningCondition : IReturningSpecimenCondition, ISelfExplainer
{
    public IsInSetReturningCondition()
    {
    }

    public IsInSetReturningCondition(string valueName, IList<object> values)
    {
        ValueName = valueName;
        Values = values;
    }

    public string ValueName { get; set; } = string.Empty;
    public IList<object> Values { get; set; } = [];

    public bool IsReturning(Specimen specimen)
    {
        return specimen.Observations.Any(IsInSet);
    }

    public string Explain()
    {
        return $"Którakolwiek z obserwacji osobnika musi mieć wartość {ValueName} z zestawu: {string.Join(", ", Values)}";
    }

    private bool IsInSet(Observation observation)
    {
        var value = observation.GetValue(ValueName);
        return Values.Any(v => ComparisonHelpers.IsEqual(v, value));
    }
}