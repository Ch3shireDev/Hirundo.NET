using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.IsInTimeBlock;

public class IsInTimeBlockModel(IsInTimeBlockCondition condition, IDataLabelRepository repository) : ParametersModel(condition, repository)
{
    public string ValueName
    {
        get => condition.ValueName;
        set => condition.ValueName = value;
    }

    public int StartHour
    {
        get => condition.TimeBlock.StartHour;
        set => condition.TimeBlock.StartHour = value;
    }

    public int EndHour
    {
        get => condition.TimeBlock.EndHour;
        set => condition.TimeBlock.EndHour = value;
    }
}