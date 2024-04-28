using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.AfterTimePeriod;

public class AfterTimePeriodModel(ReturnsAfterTimePeriodCondition condition, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersModel(condition, labelsRepository, speciesRepository)
{
    public int TimePeriodInDays
    {
        get => condition.TimePeriodInDays;
        set => condition.TimePeriodInDays = value;
    }
}