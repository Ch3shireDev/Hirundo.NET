using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.WPF.Population.IsPopulationInDateRange;

public class IsPopulationInDateRangeModel(IsPopulationInDateRangeCondition condition, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersModel(condition, labelsRepository, speciesRepository)
{
    public int MonthFrom
    {
        get => condition.MonthFrom;
        set => condition.MonthFrom = value;
    }
    
    public int MonthTo
    {
        get => condition.MonthTo;
        set => condition.MonthTo = value;
    }
    
    public int DayFrom
    {
        get => condition.DayFrom;
        set => condition.DayFrom = value;
    }
    
    public int DayTo
    {
        get => condition.DayTo;
        set => condition.DayTo = value;
    }
}