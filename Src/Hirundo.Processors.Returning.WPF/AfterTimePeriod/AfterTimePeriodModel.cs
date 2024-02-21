using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.AfterTimePeriod;

public class AfterTimePeriodModel(ReturnsAfterTimePeriodCondition condition, IDataLabelRepository repository) : ParametersModel(condition, repository)
{
    public string DateValueName
    {
        get => Condition.DateValueName;
        set => Condition.DateValueName = value;
    }

    public int TimePeriodInDays
    {
        get => Condition.TimePeriodInDays;
        set => Condition.TimePeriodInDays = value;
    }

    public ReturnsAfterTimePeriodCondition Condition { get; } = condition;
    public DataType ValueType { get; set; }
}