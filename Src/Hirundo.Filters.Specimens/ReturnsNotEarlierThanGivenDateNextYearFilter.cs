using Hirundo.Commons;

namespace Hirundo.Filters.Specimens;

[TypeDescription("ReturnsNotEarlierThanGivenDateNextYear")]
public class ReturnsNotEarlierThanGivenDateNextYearFilter(string dateValueName, int month, int day) : IReturningSpecimenFilter
{
    public string DateValueName { get; } = dateValueName;
    public int Month { get; } = month;
    public int Day { get; } = day;

    public bool IsReturning(Specimen specimen)
    {
        if (specimen.Observations.Length < 2)
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