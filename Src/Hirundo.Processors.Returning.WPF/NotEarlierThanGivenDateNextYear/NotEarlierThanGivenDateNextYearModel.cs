using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.NotEarlierThanGivenDateNextYear;

public class NotEarlierThanGivenDateNextYearModel(ReturnsNotEarlierThanGivenDateNextYearCondition condition, IDataLabelRepository repository) : ParametersModel(condition, repository)
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