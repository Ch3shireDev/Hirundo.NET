using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Population.Conditions;

[TypeDescription(
    "IsEqual",
    "Czy wartość jest równa?",
    "Warunek sprawdzający, czy osobnik z populacji ma wartość pierwszej obserwacji równą zadanej wartości."
)]
public class IsEqualPopulationCondition : IPopulationCondition, ISelfExplainer
{
    public string ValueName { get; set; } = "";
    public object? Value { get; set; }

    public IPopulationConditionClosure GetPopulationConditionClosure(Specimen returningSpecimen)
    {
        return new CustomClosure(specimen => ComparisonHelpers.IsEqual(specimen.Observations[0].GetValue(ValueName), Value));
    }

    public string Explain()
    {
        return $"Wartość pierwszej obserwacji osobnika w populacji z pola '{ValueName}' musi być równa '{Value}'.";
    }
}