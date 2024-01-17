using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF;

public class ObservationsViewModel(ObservationsModel model) : ViewModelBase
{
    public IList<ConditionViewModel> ConditionViewModels => model
        .ObservationsParameters
        .Conditions
        .Select(Create)
        .ToList();

    public IList<Type> ConditionTypes { get; } = [typeof(IsEqualFilter), typeof(IsInTimeBlockFilter)];
    public Type SelectedCondition { get; set; } = typeof(IsEqualFilter);
    public ICommand AddConditionCommand => new RelayCommand(AddCondition);

    private ConditionViewModel Create(IObservationFilter condition)
    {
        var viewModel = ConditionViewModelFactory.Create(condition);

        if (viewModel is IRemovable<IObservationFilter> removable)
        {
            removable.Removed += (_, args) =>
            {
                model.ObservationsParameters.Conditions.Remove(args);
                OnPropertyChanged(nameof(ConditionViewModels));
            };
        }

        return viewModel;
    }

    public void AddCondition()
    {
        model.AddCondition(SelectedCondition);
        OnPropertyChanged(nameof(ConditionViewModels));
    }
}