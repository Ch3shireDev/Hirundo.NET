using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.WPF.Returning.NotEarlierThanGivenDateNextYear;

public class NotEarlierThanGivenDateNextYearModel(ReturnsNotEarlierThanGivenDateNextYearCondition condition, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersModel(condition, labelsRepository, speciesRepository)
{
    public int Month
    {
        get => condition.Month;
        set => condition.Month = value;
    }

    public int Day
    {
        get => condition.Day;
        set => condition.Day = value;
    }
}