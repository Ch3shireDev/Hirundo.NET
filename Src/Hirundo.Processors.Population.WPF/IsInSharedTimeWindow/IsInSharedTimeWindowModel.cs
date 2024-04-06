using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF.IsInSharedTimeWindow;

public class IsInSharedTimeWindowModel(IsInSharedTimeWindowConditionBuilder conditionBuilder, ILabelsRepository repository) : ParametersModel(conditionBuilder, repository)
{
    public int MaxTimeDistanceInDays
    {
        get => conditionBuilder.MaxTimeDistanceInDays;
        set => conditionBuilder.MaxTimeDistanceInDays = value;
    }
    public DataType ValueType { get; set; }
}
