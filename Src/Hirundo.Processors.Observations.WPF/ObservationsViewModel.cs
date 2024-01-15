using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF;

public class ObservationsViewModel(ObservationsModel model) : ViewModelBase
{
    public IList<ConditionViewModel> ConditionViewModels { get; } = model.ObservationsParameters.Conditions.Select(ConditionViewModelFactory.Create).ToList();
    public int Count => ConditionViewModels.Count;
}