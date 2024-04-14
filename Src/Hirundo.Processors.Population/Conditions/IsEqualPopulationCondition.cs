using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Population.Conditions;

[TypeDescription(
    "IsEqual",
    "Czy wartość jest równa?",
     "Warunek sprawdzający, czy osobnik z populacji ma wartość pierwszej obserwacji równą zadanej wartości."
    )]
public class IsEqualPopulationCondition : IPopulationCondition
{
    public string ValueName { get; set; } = "";
    public object? Value { get; set; }
    public IPopulationConditionClosure GetPopulationConditionClosure(Specimen returningSpecimen)
    {
        return new CustomClosure((specimen) =>
        {
            return ComparisonHelpers.IsEqual(specimen.Observations[0].GetValue(ValueName), Value);
        });
    }
}
