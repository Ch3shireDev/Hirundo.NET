using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.NotEarlierThanGivenDateNextYear;

internal class NotEarlierThanGivenDateNextYearViewModel(NotEarlierThanGivenDateNextYearModel model) : ReturningSpecimensConditionViewModel, IRemovable<IReturningSpecimenFilter>
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

    public int Month
    {
        get => model.Month;
        set
        {
            model.Month = value;
            OnPropertyChanged();
        }
    }

    public int Day
    {
        get => model.Day;
        set
        {
            model.Day = value;
            OnPropertyChanged();
        }
    }

    public ICommand RemoveCommand => new RelayCommand(Remove);
    public event EventHandler<IReturningSpecimenFilter>? Removed;

    public void Remove()
    {
        Removed?.Invoke(this, model.Condition);
    }
}