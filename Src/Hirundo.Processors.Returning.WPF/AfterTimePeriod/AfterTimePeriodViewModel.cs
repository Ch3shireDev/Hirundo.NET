using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.AfterTimePeriod;

[ParametersData(
    typeof(ReturnsAfterTimePeriodCondition),
    typeof(AfterTimePeriodModel),
    typeof(AfterTimePeriodView),
    "Czy powrót nastąpił po określonym czasie?",
    "Osobnik wraca nie wcześniej niż po określonej liczbie dni."
)]
public class AfterTimePeriodViewModel(AfterTimePeriodModel model) : ParametersViewModel, IRemovable
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

    public int TimePeriodInDays
    {
        get => model.TimePeriodInDays;
        set
        {
            model.TimePeriodInDays = value;
            OnPropertyChanged();
        }
    }

    public override ICommand RemoveCommand => new RelayCommand(() => Remove(model.Condition));
}