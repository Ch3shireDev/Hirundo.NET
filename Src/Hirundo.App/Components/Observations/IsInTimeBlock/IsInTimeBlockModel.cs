using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.App.Components.Observations.IsInTimeBlock;

internal class IsInTimeBlockModel
{
    public IsInTimeBlockModel()
    {
    }

    public IsInTimeBlockModel(IsInTimeBlockFilter filter)
    {
        ValueName = filter.ValueName;
        StartHour = filter.TimeBlock.StartHour;
        EndHour = filter.TimeBlock.EndHour;
    }

    public string ValueName { get; set; } = null!;
    public int StartHour { get; set; } = 6;
    public int EndHour { get; set; } = 12;
}