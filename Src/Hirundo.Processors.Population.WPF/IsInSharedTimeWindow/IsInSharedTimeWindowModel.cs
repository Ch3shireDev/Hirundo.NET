using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF.IsInSharedTimeWindow;

public class IsInSharedTimeWindowModel(IsInSharedTimeWindowCondition conditionBuilder, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository) : ParametersModel(conditionBuilder, labelsRepository, speciesRepository)
{
    public int MaxTimeDistanceInDays
    {
        get => conditionBuilder.MaxTimeDistanceInDays;
        set => conditionBuilder.MaxTimeDistanceInDays = value;
    }
    public DataType ValueType { get; set; }
}
