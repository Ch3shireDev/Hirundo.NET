using Hirundo.Commons;

namespace Hirundo.Processors.Observations.Conditions;

[TypeDescription("IsInSeason")]
public class IsInSeasonFilter(string dateColumnName, Season season) : IObservationFilter
{
    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        var dateTimeValue = observation.GetValue(dateColumnName);

        if (dateTimeValue is DateTime dateTime)
        {
            return IsInDateRange(dateTime);
        }

        throw new ArgumentException($"Value of column {dateColumnName} is not a DateTime or string");
    }

    private bool IsInDateRange(DateTime date)
    {
        var year = date.Year;

        var seasonStart = new DateTime(year, season.StartMonth, season.StartDay);
        var seasonEnd = new DateTime(year, season.EndMonth, season.EndDay);

        return date >= seasonStart && date <= seasonEnd;
    }
}