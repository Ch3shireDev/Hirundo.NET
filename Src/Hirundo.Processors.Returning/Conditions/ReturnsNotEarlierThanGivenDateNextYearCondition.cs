﻿using Hirundo.Commons;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription(
    "ReturnsNotEarlierThanGivenDateNextYear",
    "Czy powrót nastąpił po określonej dacie kolejnego roku?",
    "Osobnik wraca nie wcześniej niż w określonej dacie w przyszłym roku.")]
public class ReturnsNotEarlierThanGivenDateNextYearCondition : IReturningSpecimenCondition
{
    public ReturnsNotEarlierThanGivenDateNextYearCondition()
    {
    }

    public ReturnsNotEarlierThanGivenDateNextYearCondition(int month, int day)
    {
        Month = month;
        Day = day;
    }

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
            .Select(o => o.Date)
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