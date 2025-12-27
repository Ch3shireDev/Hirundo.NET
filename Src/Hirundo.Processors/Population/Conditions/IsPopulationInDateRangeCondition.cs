using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Population.Conditions;

[TypeDescription(
    "IsPopulationInDateRange",
    "Czy data jest w przedziale?",
    "Warunek sprawdzający, czy osobnik z populacji ma datę obserwacji w przedziale dat."
)]
public class IsPopulationInDateRangeCondition : IPopulationCondition, ISelfExplainer
{ 
    public int MonthFrom { get; set; }
    public int DayFrom { get; set; }
    public int MonthTo { get; set; }
    public int DayTo { get; set; }

    public IPopulationConditionClosure GetPopulationConditionClosure(Specimen returningSpecimen)
    {
        return new CustomClosure(specimen => 
            ComparisonHelpers.IsGreaterOrEqual(specimen.Observations[0].Date.Month, MonthFrom) && 
            ComparisonHelpers.IsGreaterOrEqual(specimen.Observations[0].Date.Day, DayFrom) && 
            ComparisonHelpers.IsLowerOrEqual(specimen.Observations[0].Date.Month, MonthTo) && 
            ComparisonHelpers.IsLowerOrEqual(specimen.Observations[0].Date.Month, DayTo) 
        );
    }

    public string Explain()
    {
        return $"Data pierwszej obserwacji osobnika w populacji z pola daty musi być większa lub równa '{MonthFrom:D2}.{DayFrom:D2}' oraz mniejsza lub równa '{MonthTo:D2}.{DayTo:D2}'.";
    }
}