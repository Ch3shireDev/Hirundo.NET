using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Returning.WPF;

public class ReturningSpecimensViewModel(ReturningSpecimensModel model) : ViewModelBase
{
    public IList<ReturningSpecimensConditionViewModel> ConditionsViewModels { get; } = model.Conditions.Select(ReturningSpecimensConditionFactory.Create).ToList();
}