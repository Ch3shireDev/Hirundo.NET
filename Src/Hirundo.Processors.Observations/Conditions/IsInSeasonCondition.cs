using Hirundo.Commons;

namespace Hirundo.Processors.Observations.Conditions;

[TypeDescription("IsInSeason")]
public class IsInSeasonCondition(string dateColumnName, Season season) : IObservationCondition
{
    public string DateColumnName { get; } = dateColumnName;
    public Season Season { get; } = season;

    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        var dateTimeValue = observation.GetValue(DateColumnName);

        if (dateTimeValue is DateTime dateTime)
        {
            return IsInDateRange(dateTime);
        }

        throw new ArgumentException($"Value of column {DateColumnName} is not a DateTime or string");
    }

    private bool IsInDateRange(DateTime date)
    {
        var year = date.Year;

        var seasonStart = new DateTime(year, Season.StartMonth, Season.StartDay);
        var seasonEnd = new DateTime(year, Season.EndMonth, Season.EndDay);

        return date >= seasonStart && date <= seasonEnd;
    }
}