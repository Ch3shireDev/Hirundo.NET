using Hirundo.Commons;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription("ReturnsNotEarlierThanGivenDateNextYear")]
public class ReturnsNotEarlierThanGivenDateNextYearCondition : IReturningSpecimenCondition
{
    public ReturnsNotEarlierThanGivenDateNextYearCondition()
    {
    }

    public ReturnsNotEarlierThanGivenDateNextYearCondition(string dateValueName, int month, int day)
    {
        DateValueName = dateValueName;
        Month = month;
        Day = day;
    }

    public string DateValueName { get; set; } = "DATE";
    public int Month { get; set; } = 06;
    public int Day { get; set; } = 01;

    public bool IsReturning(Specimen specimen)
    {
        ArgumentNullException.ThrowIfNull(specimen);

        if (specimen.Observations.Count < 2)
        {
            return false;
        }

        var dates = specimen.Observations
            .Select(o => o.GetValue<DateTime>(DateValueName).Date)
            .OrderBy(d => d)
            .ToList();

        var datesPairs = dates.Zip(dates.Skip(1));

        return datesPairs.Any(pair => IsConditionForDatesMet(pair.First, pair.Second));
    }

    private bool IsConditionForDatesMet(DateTime firstDate, DateTime lastDate)
    {
        var expectedReturnDate = new DateTime(firstDate.Year + 1, Month, Day);

        return lastDate >= expectedReturnDate;
    }
}