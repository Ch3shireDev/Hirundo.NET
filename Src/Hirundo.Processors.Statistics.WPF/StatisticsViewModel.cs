using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Statistics.WPF;

public class StatisticsViewModel(StatisticsModel model) : ViewModelBase
{
    public IList<OperationViewModel> OperationsViewModels { get; } = model.StatisticsProcessorParameters.Operations.Select(OperationViewModelFactory.Create).ToList();
}