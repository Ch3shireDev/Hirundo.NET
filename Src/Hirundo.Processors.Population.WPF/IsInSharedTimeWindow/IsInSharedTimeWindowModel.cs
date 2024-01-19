using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF.IsInSharedTimeWindow;

public class IsInSharedTimeWindowModel(IsInSharedTimeWindowConditionBuilder condition)
{
    public string DateValueName
    {
        get => Condition.DateValueName;
        set => Condition.DateValueName = value;
    }

    public int MaxTimeDistanceInDays
    {
        get => Condition.MaxTimeDistanceInDays;
        set => Condition.MaxTimeDistanceInDays = value;
    }

    public IsInSharedTimeWindowConditionBuilder Condition { get; set; } = condition;
}