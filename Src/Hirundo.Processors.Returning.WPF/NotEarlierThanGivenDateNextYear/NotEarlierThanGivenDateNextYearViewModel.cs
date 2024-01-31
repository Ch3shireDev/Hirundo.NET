using System.Windows.Input;
using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;

namespace Hirundo.Processors.Returning.WPF.NotEarlierThanGivenDateNextYear;

public class NotEarlierThanGivenDateNextYearViewModel(NotEarlierThanGivenDateNextYearModel model) : ParametersViewModel, IRemovable
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
    public IDataLabelRepository Repository => model.Repository;

    public DataType DataType
    {
        get => model.ValueType;
        set
        {
            model.ValueType = value;
            OnPropertyChanged();
        }
    }

    public event EventHandler<ParametersEventArgs>? Removed;

    public void Remove()
    {
        Removed?.Invoke(this, new ParametersEventArgs(model.Condition));
    }
}