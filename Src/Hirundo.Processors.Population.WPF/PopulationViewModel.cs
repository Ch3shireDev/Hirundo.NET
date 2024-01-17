using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF;

public class PopulationViewModel(PopulationModel model) : ViewModelBase
{
    public IList<PopulationConditionViewModel> ConditionsViewModels => [.. model.Conditions.Select(CreateViewModel)];

    public IList<Type> ConditionTypes { get; } =
    [
        typeof(IsInSharedTimeWindowFilterBuilder)
    ];

    public Type SelectedConditionType { get; set; } = typeof(IsInSharedTimeWindowFilterBuilder);

    public ICommand AddConditionCommand => new RelayCommand(AddCondition);

    public PopulationConditionViewModel CreateViewModel(IPopulationFilterBuilder condition)
    {
        var viewModel = PopulationConditionFactory.Create(condition);

        if (viewModel is IRemovable<IPopulationFilterBuilder> removable)
        {
            removable.Removed += (_, args) =>
            {
                model.Conditions.Remove(args);
                OnPropertyChanged(nameof(ConditionsViewModels));
            };
        }

        return viewModel;
    }

    public void AddCondition()
    {
        model.AddCondition(SelectedConditionType);
        OnPropertyChanged(nameof(ConditionsViewModels));
    }
}