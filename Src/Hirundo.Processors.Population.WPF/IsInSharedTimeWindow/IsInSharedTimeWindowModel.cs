using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF.IsInSharedTimeWindow;

public class IsInSharedTimeWindowModel(IsInSharedTimeWindowFilterBuilder condition)
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

    public IsInSharedTimeWindowFilterBuilder Condition { get; set; } = condition;
}