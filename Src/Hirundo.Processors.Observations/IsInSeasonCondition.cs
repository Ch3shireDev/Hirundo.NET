using Hirundo.Commons;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

[TypeDescription("IsInSeason",
    "Czy dane są w sezonie?",
    "Sprawdza, czy dana obserwacja zaszła w zadanym przedziale dat, dowolnego roku.")]
public class IsInSeasonCondition : IObservationCondition
{
    public IsInSeasonCondition(Season season)
    {
        Season = season;
    }

    public IsInSeasonCondition()
    {
    }

    public Season Season { get; set; } = new();

    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);

        return IsInDateRange(observation.Date);
    }

    private bool IsInDateRange(DateTime date)
    {
        var year = date.Year;

        var seasonStart = new DateTime(year, Season.StartMonth, Season.StartDay);
        var seasonEnd = new DateTime(year, Season.EndMonth, Season.EndDay);

        return date >= seasonStart && date <= seasonEnd;
    }
}