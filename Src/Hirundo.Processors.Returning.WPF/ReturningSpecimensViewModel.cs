using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Returning.WPF;

public class ReturningSpecimensViewModel : ViewModelBase
{
    public ReturningSpecimensViewModel(ReturningSpecimensModel model)
    {
        ConditionsViewModels = model.Conditions.Select(ReturningSpecimensConditionFactory.Create).ToList();
    }

    public IList<ReturningSpecimensConditionViewModel> ConditionsViewModels { get; } = [];
}