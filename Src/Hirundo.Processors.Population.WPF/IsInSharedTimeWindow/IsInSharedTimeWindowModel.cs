using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF.IsInSharedTimeWindow;

public class IsInSharedTimeWindowModel(IsInSharedTimeWindowConditionBuilder conditionBuilder, IDataLabelRepository repository) : ParametersModel(conditionBuilder, repository)
{
    public string DateValueName
    {
        get => ConditionBuilder.DateValueName;
        set => ConditionBuilder.DateValueName = value;
    }

    public int MaxTimeDistanceInDays
    {
        get => ConditionBuilder.MaxTimeDistanceInDays;
        set => ConditionBuilder.MaxTimeDistanceInDays = value;
    }

    public IsInSharedTimeWindowConditionBuilder ConditionBuilder { get; set; } = conditionBuilder;
    public DataType ValueType { get; set; }
}