using Hirundo.Commons;

namespace Hirundo.Filters.Observations;

[Polymorphic("IsInTimeBlock")]
public class IsInTimeBlockFilter(string valueName, TimeBlock timeBlock) : IObservationFilter
{
    private readonly bool isThroughMidnight = timeBlock.StartHour > timeBlock.EndHour;
    public string ValueName { get; } = valueName;
    public TimeBlock TimeBlock { get; } = timeBlock;

    public bool IsAccepted(Observation observation)
    {
        var dateTimeValue = observation.GetValue(ValueName);

        if (dateTimeValue is int hour)
        {
            return IsInTimeRange(hour);
        }

        throw new ArgumentException($"Value of column {ValueName} is not a DateTime or string");
    }

    private bool IsInTimeRange(int hour)
    {
        if (isThroughMidnight)
        {
            return hour >= TimeBlock.StartHour || hour <= TimeBlock.EndHour;
        }

        return hour >= TimeBlock.StartHour && hour <= TimeBlock.EndHour;
    }
}