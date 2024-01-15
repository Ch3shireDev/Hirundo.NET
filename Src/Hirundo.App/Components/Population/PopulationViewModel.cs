using Hirundo.App.Components.Population.IsInSharedTimeWindow;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.App.Components.Population;

public class PopulationViewModel(PopulationModel model) : ViewModelBase
{
    public IList<PopulationConditionViewModel> ConditionsViewModels { get; } = model.Conditions.Select(PopulationConditionFactory.Create).ToList();
}

public static class PopulationConditionFactory
{
    public static PopulationConditionViewModel Create(IPopulationFilterBuilder condition)
        => condition switch
        {
            IsInSharedTimeWindowFilterBuilder m => new IsInSharedTimeWindowViewModel(new IsInSharedTimeWindowModel(m)),
            _ => throw new NotSupportedException($"Condition of type {condition.GetType()} is not supported.")
        };
}