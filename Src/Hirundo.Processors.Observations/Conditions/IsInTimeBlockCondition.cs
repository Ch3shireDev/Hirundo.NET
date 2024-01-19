using System.Globalization;
using Hirundo.Commons;

namespace Hirundo.Processors.Observations.Conditions;

[TypeDescription("IsInTimeBlock")]
public class IsInTimeBlockCondition : IObservationCondition
{
    private readonly bool isThroughMidnight;

    public IsInTimeBlockCondition()
    {
    }

    public IsInTimeBlockCondition(string valueName, TimeBlock timeBlock, bool rejectNullValues = false)
    {
        isThroughMidnight = timeBlock.StartHour > timeBlock.EndHour;
        ValueName = valueName;
        TimeBlock = timeBlock;
        RejectNullValues = rejectNullValues;
    }

    public string ValueName { get; set; } = null!;
    public TimeBlock TimeBlock { get; set; } = new TimeBlock(6, 12);
    public bool RejectNullValues { get; set; }

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