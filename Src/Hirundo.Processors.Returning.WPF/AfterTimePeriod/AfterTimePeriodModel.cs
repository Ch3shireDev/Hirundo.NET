using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.AfterTimePeriod;

internal class AfterTimePeriodModel(ReturnsAfterTimePeriodFilter condition)
{
    public string DateValueName
    {
        get => Condition.DateValueName;
        set => Condition.DateValueName = value;
    }

    public int TimePeriodInDays
    {
        get => Condition.TimePeriodInDays;
        set => Condition.TimePeriodInDays = value;
    }

    public ReturnsAfterTimePeriodFilter Condition { get; } = condition;
}