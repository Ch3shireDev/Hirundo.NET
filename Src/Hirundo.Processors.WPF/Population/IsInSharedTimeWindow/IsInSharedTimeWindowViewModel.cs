using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.WPF.Population.IsInSharedTimeWindow;

[ParametersData(
    typeof(IsInSharedTimeWindowCondition),
    typeof(IsInSharedTimeWindowModel),
    typeof(IsInSharedTimeWindowView)
)]
public class IsInSharedTimeWindowViewModel(IsInSharedTimeWindowModel model) : ParametersViewModel(model)
{
    public int MaxTimeDistanceInDays
    {
        get => model.MaxTimeDistanceInDays;
        set
        {
            model.MaxTimeDistanceInDays = value;
            OnPropertyChanged();
        }
    }
}