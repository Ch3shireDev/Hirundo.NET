using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.NotEarlierThanGivenDateNextYear;

public class NotEarlierThanGivenDateNextYearModel(ReturnsNotEarlierThanGivenDateNextYearFilter condition)
{
    public ReturnsNotEarlierThanGivenDateNextYearFilter Condition { get; set; } = condition;

    public string DateValueName
    {
        get => Condition.DateValueName;
        set => Condition.DateValueName = value;
    }

    public int Month
    {
        get => Condition.Month;
        set => Condition.Month = value;
    }

    public int Day
    {
        get => Condition.Day;
        set => Condition.Day = value;
    }
}