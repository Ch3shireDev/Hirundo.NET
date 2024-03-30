using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF.IsInSharedTimeWindow;

public class IsInSharedTimeWindowModel(IsInSharedTimeWindowConditionBuilder conditionBuilder, IDataLabelRepository repository) : ParametersModel(conditionBuilder, repository)
{
    public string DateValueName
    {
        get => conditionBuilder.DateValueName;
        set => conditionBuilder.DateValueName = value;
    }

    public int MaxTimeDistanceInDays
    {
        get => conditionBuilder.MaxTimeDistanceInDays;
        set => conditionBuilder.MaxTimeDistanceInDays = value;
    }
    public DataType ValueType { get; set; }
}
