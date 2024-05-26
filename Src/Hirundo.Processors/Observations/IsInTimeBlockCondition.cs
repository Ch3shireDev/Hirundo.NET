using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;
using System.Globalization;

namespace Hirundo.Processors.Observations;

[TypeDescription(
    "IsInTimeBlock",
    "Czy dane są w przedziale godzinowym?",
    "Warunek sprawdzający godziny złapania osobnika."
)]
public class IsInTimeBlockCondition : IObservationCondition, ISelfExplainer
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
    public TimeBlock TimeBlock { get; set; } = new(6, 12);
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

    public string Explain()
    {
        var acceptType = RejectNullValues ? "odrzucane" : "zatwierdzane";

        return $"Wartość {ValueName} musi być w przedziale godzin od {TimeBlock.StartHour} do {TimeBlock.EndHour}. Puste wartości {ValueName} są {acceptType}.";
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