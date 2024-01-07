using Hirundo.Commons;

namespace Hirundo.Filters.Observations;

public class IsInTimeBlockFilter(string columnName, TimeBlock block) : IObservationFilter
{
    private readonly bool isThroughMidnight = block.StartHour > block.EndHour;

    public bool IsSelected(Observation observation)
    {
        var dateTimeValue = observation.GetValue(columnName);

        if (dateTimeValue is int hour)
        {
            return IsInTimeRange(hour);
        }

        throw new ArgumentException($"Value of column {columnName} is not a DateTime or string");
    }

    private bool IsInTimeRange(int hour)
    {
        if (isThroughMidnight)
        {
            return hour >= block.StartHour || hour <= block.EndHour;
        }

        return hour >= block.StartHour && hour <= block.EndHour;
    }
}