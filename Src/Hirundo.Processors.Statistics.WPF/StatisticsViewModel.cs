using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics.WPF;

public class StatisticsViewModel(StatisticsModel model) : ViewModelBase
{
    public IList<OperationViewModel> OperationsViewModels => model
        .StatisticsProcessorParameters
        .Operations
        .Select(Create)
        .ToList();

    public ICommand AddOperationCommand => new RelayCommand(AddOperation);
    public IList<Type> OperationTypes { get; } = [typeof(AverageOperation)];
    public Type SelectedType { get; set; } = typeof(AverageOperation);

    public OperationViewModel Create(IStatisticalOperation operation)
    {
        var viewModel = OperationViewModelFactory.Create(operation);

        if (viewModel is IRemovable<IStatisticalOperation> removable)
        {
            removable.Removed += (_, args) =>
            {
                model.StatisticsProcessorParameters.Operations.Remove(args);
                OnPropertyChanged(nameof(OperationsViewModels));
            };
        }

        return viewModel;
    }

    public void AddOperation()
    {
        model.AddOperation(SelectedType);
        OnPropertyChanged(nameof(OperationsViewModels));
    }
}