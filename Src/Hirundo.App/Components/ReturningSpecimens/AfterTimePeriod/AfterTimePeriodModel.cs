using Hirundo.Filters.Specimens;

namespace Hirundo.App.Components.ReturningSpecimens.AfterTimePeriod;

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