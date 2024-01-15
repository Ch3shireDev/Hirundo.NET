using Hirundo.Processors.Population.Conditions;

namespace Hirundo.App.Components.Population.IsInSharedTimeWindow;

public class IsInSharedTimeWindowModel
{
    public IsInSharedTimeWindowModel(IsInSharedTimeWindowFilterBuilder condition)
    {
        Condition = condition;
    }

    public IsInSharedTimeWindowFilterBuilder Condition
    {
        get => new(DateValueName, MaxTimeDistanceInDays);

        set
        {
            ArgumentNullException.ThrowIfNull(value);
            DateValueName = value.DateValueName;
            MaxTimeDistanceInDays = value.MaxTimeDistanceInDays;
        }
    }

    public string DateValueName { get; set; } = null!;
    public int MaxTimeDistanceInDays { get; set; }
}