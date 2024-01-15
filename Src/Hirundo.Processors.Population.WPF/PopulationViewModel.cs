using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Population.WPF;

public class PopulationViewModel(PopulationModel model) : ViewModelBase
{
    public IList<PopulationConditionViewModel> ConditionsViewModels { get; } = model.Conditions.Select(PopulationConditionFactory.Create).ToList();
}