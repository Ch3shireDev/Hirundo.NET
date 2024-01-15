using Hirundo.Processors.Population.Conditions;
using Hirundo.Processors.Population.WPF.IsInSharedTimeWindow;

namespace Hirundo.Processors.Population.WPF;

public static class PopulationConditionFactory
{
    public static PopulationConditionViewModel Create(IPopulationFilterBuilder condition)
    {
        return condition switch
        {
            IsInSharedTimeWindowFilterBuilder m => new IsInSharedTimeWindowViewModel(new IsInSharedTimeWindowModel(m)),
            _ => throw new NotSupportedException($"Condition of type {condition.GetType()} is not supported.")
        };
    }
}