using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.AfterTimePeriod;

internal class AfterTimePeriodModel
{
    public AfterTimePeriodModel()
    {
    }

    public AfterTimePeriodModel(ReturnsAfterTimePeriodFilter condition)
    {
        Condition = condition;
    }

    public ReturnsAfterTimePeriodFilter Condition
    {
        get => new(DateValueName, TimePeriodInDays);
        set
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            DateValueName = value.DateValueName;
            TimePeriodInDays = value.TimePeriodInDays;
        }
    }

    public string DateValueName { get; set; } = null!;
    public int TimePeriodInDays { get; set; } = 365;
}