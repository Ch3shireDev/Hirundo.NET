using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.NotEarlierThanGivenDateNextYear;

public class NotEarlierThanGivenDateNextYearModel(ReturnsNotEarlierThanGivenDateNextYearCondition condition, IDataLabelRepository repository) : ParametersModel(condition, repository)
{
    public string DateValueName
    {
        get => condition.DateValueName;
        set => condition.DateValueName = value;
    }

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

    public DataType ValueType { get; set; }
}