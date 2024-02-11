using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsEqual;

[ParametersData(
    typeof(IsEqualObservationCondition),
    typeof(IsEqualModel),
    typeof(IsEqualView),
    "Czy wartość jest równa?",
    "Warunek porównujący pole danych z podaną wartością."
)]
public class IsEqualViewModel(IsEqualModel model) : ParametersViewModel, IRemovable
{
    public IsEqualModel Model => model;

    public string Value
    {
        get => model.ValueStr;
        set
        {
            model.ValueStr = value;
            OnPropertyChanged();
        }
    }

    public string ValueName
    {
        get => model.ValueName;
        set
        {
            model.ValueName = value;
            OnPropertyChanged();
        }
    }

    public IDataLabelRepository Repository => model.Repository;

    public DataType DataType
    {
        get => model.DataType;
        set
        {
            model.DataType = value;
            OnPropertyChanged();
        }
    }

    public override ICommand RemoveCommand => new RelayCommand(() => Remove(model.ObservationCondition));
}