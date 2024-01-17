using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF;

public class ReturningSpecimensViewModel(ReturningSpecimensModel model) : ViewModelBase
{
    public IList<ReturningSpecimensConditionViewModel> ConditionsViewModels => model
        .Conditions
        .Select(Create)
        .ToList();

    public IList<Type> ReturningTypes { get; } = [typeof(ReturnsAfterTimePeriodFilter), typeof(ReturnsNotEarlierThanGivenDateNextYearFilter)];
    public Type SelectedReturningType { get; set; } = typeof(ReturnsAfterTimePeriodFilter);

    public ICommand AddConditionCommand => new RelayCommand(AddCondition);

    public ReturningSpecimensConditionViewModel Create(IReturningSpecimenFilter condition)
    {
        var viewModel = ReturningSpecimensConditionFactory.Create(condition);

        if (viewModel is IRemovable<IReturningSpecimenFilter> removable)
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
        model.AddCondition(SelectedReturningType);
        OnPropertyChanged(nameof(ConditionsViewModels));
    }
}