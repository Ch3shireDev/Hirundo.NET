using System.Globalization;
using Hirundo.Commons;
using Serilog;

namespace Hirundo.Filters.Observations;

[TypeDescription("IsInTimeBlock")]
public class IsInTimeBlockFilter(string valueName, TimeBlock timeBlock, bool rejectNullValues = false) : IObservationFilter
{
    private readonly bool isThroughMidnight = timeBlock.StartHour > timeBlock.EndHour;
    public string ValueName { get; } = valueName;
    public TimeBlock TimeBlock { get; } = timeBlock;
    public bool RejectNullValues { get; } = rejectNullValues;

    public bool IsAccepted(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        var hourValue = observation.GetValue(ValueName);

        if (hourValue == null)
        {
            return !RejectNullValues;
        }

        if (IsNumeric(hourValue))
        {
            var hour = Convert.ToInt32(hourValue, CultureInfo.InvariantCulture);
            return IsInTimeRange(hour);
        }

        throw new ArgumentException($"Wartość kolumny {ValueName} nie jest typem numerycznym.");
    }

    private static bool IsNumeric(object hourValue)
    {
        var type = hourValue.GetType();
        var isNumeric = type.IsPrimitive || type == typeof(decimal);
        return isNumeric;
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