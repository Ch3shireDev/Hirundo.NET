using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.AfterTimePeriod;

internal class AfterTimePeriodViewModel(AfterTimePeriodModel model) : ReturningSpecimensConditionViewModel, IRemovable<IReturningSpecimenFilter>
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

    public int TimePeriodInDays
    {
        get => model.TimePeriodInDays;
        set
        {
            model.TimePeriodInDays = value;
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