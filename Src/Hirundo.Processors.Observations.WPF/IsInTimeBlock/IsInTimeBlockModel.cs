using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsInTimeBlock;

public class IsInTimeBlockModel(IsInTimeBlockCondition condition, IDataLabelRepository repository) : ParametersModel(condition, repository)
{
    public IsInTimeBlockCondition Condition { get; set; } = condition;

    public string ValueName
    {
        get => Condition.ValueName;
        set => Condition.ValueName = value;
    }

    public int StartHour
    {
        get => Condition.TimeBlock.StartHour;
        set => Condition.TimeBlock.StartHour = value;
    }

    public int EndHour
    {
        get => Condition.TimeBlock.EndHour;
        set => Condition.TimeBlock.EndHour = value;
    }
}