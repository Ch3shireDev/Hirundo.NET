using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF.IsInSharedTimeWindow;

public class IsInSharedTimeWindowViewModel(IsInSharedTimeWindowModel model) : PopulationConditionViewModel, IRemovable<IPopulationFilterBuilder>
{
    public string DateValueName
    {
        get => model.DateValueName;
        set
        {
            model.DateValueName = value;
            OnPropertyChanged();
        }
    }

    public int MaxTimeDistanceInDays
    {
        get => model.MaxTimeDistanceInDays;
        set
        {
            model.MaxTimeDistanceInDays = value;
            OnPropertyChanged();
        }
    }

    public ICommand RemoveCommand => new RelayCommand(Remove);

    public event EventHandler<IPopulationFilterBuilder>? Removed;

    public void Remove()
    {
        Removed?.Invoke(this, model.Condition);
    }
}