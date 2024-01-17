using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsInTimeBlock;

internal class IsInTimeBlockViewModel(IsInTimeBlockModel model) : ConditionViewModel, IRemovable<IObservationFilter>
{
    public string ValueName
    {
        get => model.ValueName;
        set
        {
            model.ValueName = value;
            OnPropertyChanged();
        }
    }

    public int StartHour
    {
        get => model.StartHour;
        set
        {
            model.StartHour = value;
            OnPropertyChanged();
        }
    }

    public int EndHour
    {
        get => model.EndHour;
        set
        {
            model.EndHour = value;
            OnPropertyChanged();
        }
    }

    public ICommand RemoveCommand => new RelayCommand(Remove);
    public event EventHandler<IObservationFilter>? Removed;

    public void Remove()
    {
        Removed?.Invoke(this, model.Filter);
    }

    public override string ToString()
    {
        return "Czy jest w przedziale czasowym?";
    }
}