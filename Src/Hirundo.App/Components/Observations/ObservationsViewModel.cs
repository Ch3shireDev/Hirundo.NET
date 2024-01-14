namespace Hirundo.App.Components.Observations;

public class ObservationsViewModel(ObservationsModel model) : ViewModelBase
{
    public IList<ConditionViewModel> ConditionViewModels { get; } = model.ObservationsParameters.Conditions.Select(ConditionViewModelFactory.Create).ToList();
    public int Count => ConditionViewModels.Count;
}