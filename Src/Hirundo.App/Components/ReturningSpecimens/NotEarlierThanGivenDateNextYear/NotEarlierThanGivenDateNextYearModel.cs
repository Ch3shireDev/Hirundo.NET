using Hirundo.Filters.Specimens;

namespace Hirundo.App.Components.ReturningSpecimens.NotEarlierThanGivenDateNextYear;

internal class NotEarlierThanGivenDateNextYearModel
{
    public NotEarlierThanGivenDateNextYearModel() { }

    public NotEarlierThanGivenDateNextYearModel(ReturnsNotEarlierThanGivenDateNextYearFilter condition)
    {
        Condition = condition;
    }

    public ReturnsNotEarlierThanGivenDateNextYearFilter Condition
    {
        get => new(DateValueName, Month, Day);
        set
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            DateValueName = value.DateValueName;
            Month = value.Month;
            Day = value.Day;
        }
    }

    public string DateValueName { get; set; } = null!;
    public int Month { get; set; } = 06;
    public int Day { get; set; } = 15;
}