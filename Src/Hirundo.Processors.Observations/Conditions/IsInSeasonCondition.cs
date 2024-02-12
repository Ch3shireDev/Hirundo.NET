using Hirundo.Commons;

namespace Hirundo.Processors.Observations.Conditions;

[TypeDescription("IsInSeason")]
public class IsInSeasonCondition : IObservationCondition
{
    public IsInSeasonCondition(string dateColumnName, Season season)
    {
        DateColumnName = dateColumnName;
        Season = season;
    }

    public IsInSeasonCondition()
    {
    }

    public string DateColumnName { get; set; } = string.Empty;
    public Season Season { get; set; } = new();

    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        var dateTimeValue = observation.GetValue(DateColumnName);

        if (dateTimeValue is DateTime dateTime)
        {
            return IsInDateRange(dateTime);
        }

        throw new ArgumentException($"Value of column {DateColumnName} is not a DateTime.");
    }

    private bool IsInDateRange(DateTime date)
    {
        var year = date.Year;

        var seasonStart = new DateTime(year, Season.StartMonth, Season.StartDay);
        var seasonEnd = new DateTime(year, Season.EndMonth, Season.EndDay);

        return date >= seasonStart && date <= seasonEnd;
    }
}