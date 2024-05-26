using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;

namespace Hirundo.Processors.WPF.Observations.IsInTimeBlock;

public class IsInTimeBlockModel(IsInTimeBlockCondition condition, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersModel(condition, labelsRepository, speciesRepository)
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