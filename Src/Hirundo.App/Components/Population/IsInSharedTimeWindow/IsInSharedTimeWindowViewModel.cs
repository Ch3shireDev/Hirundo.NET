namespace Hirundo.App.Components.Population.IsInSharedTimeWindow;

public class IsInSharedTimeWindowViewModel(IsInSharedTimeWindowModel model) : PopulationConditionViewModel
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
}