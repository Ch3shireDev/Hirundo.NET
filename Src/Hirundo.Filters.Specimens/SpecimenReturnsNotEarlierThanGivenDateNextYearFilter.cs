using Hirundo.Commons;

namespace Hirundo.Filters.Specimens;

public class SpecimenReturnsNotEarlierThanGivenDateNextYearFilter(string dateValueName, int month, int day) : IReturningSpecimenFilter
{
    public bool IsReturning(Specimen specimen)
    {
        if (specimen.Observations.Count < 2)
        {
            return false;
        }

        var dates = specimen.Observations
            .Select(o => o.GetValue<DateTime>(dateValueName).Date)
            .OrderBy(d => d)
            .ToList();

        var datesPairs = dates.Zip(dates.Skip(1));

        return datesPairs.Any(pair => IsConditionForDatesMet(pair.First, pair.Second));
    }

    private bool IsConditionForDatesMet(DateTime firstDate, DateTime lastDate)
    {
        var expectedReturnDate = new DateTime(firstDate.Year + 1, month, day);

        return lastDate >= expectedReturnDate;
    }
}