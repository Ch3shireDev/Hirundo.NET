using System.Windows.Input;
using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.NotEarlierThanGivenDateNextYear;

[ParametersData(
    typeof(ReturnsNotEarlierThanGivenDateNextYearCondition),
    typeof(NotEarlierThanGivenDateNextYearModel),
    typeof(NotEarlierThanGivenDateNextYearView),
    "Czy powrót nastąpił po określonej dacie kolejnego roku?",
    "Osobnik wraca nie wcześniej niż w określonej dacie w przyszłym roku"
)]
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

    public ICommand RemoveCommand => new RelayCommand(Remove);

    public event EventHandler<ParametersEventArgs>? Removed;

    public void Remove()
    {
        Removed?.Invoke(this, new ParametersEventArgs(model.Condition));
    }
}