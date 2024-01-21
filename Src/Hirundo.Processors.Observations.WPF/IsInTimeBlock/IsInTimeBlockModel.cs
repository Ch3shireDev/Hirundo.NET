using Hirundo.Commons;
using Hirundo.Processors.Observations.Conditions;
using Hirundo.Repositories.DataLabels;

namespace Hirundo.Processors.Observations.WPF.IsInTimeBlock;

public class IsInTimeBlockModel(IsInTimeBlockCondition condition, IDataLabelRepository repository)
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

    public IEnumerable<DataLabel> GetLabels()
    {
        return repository.GetLabels();
    }
}