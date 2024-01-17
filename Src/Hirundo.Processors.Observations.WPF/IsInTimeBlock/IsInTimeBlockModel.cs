using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsInTimeBlock;

internal class IsInTimeBlockModel(IsInTimeBlockFilter filter)
{
    public IsInTimeBlockFilter Filter { get; set; } = filter;

    public string ValueName
    {
        get => Filter.ValueName;
        set => Filter.ValueName = value;
    }

    public int StartHour
    {
        get => Filter.TimeBlock.StartHour;
        set => Filter.TimeBlock.StartHour = value;
    }

    public int EndHour
    {
        get => Filter.TimeBlock.EndHour;
        set => Filter.TimeBlock.EndHour = value;
    }
}