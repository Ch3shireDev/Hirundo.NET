using System.Windows.Input;
using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsEqual;

[ParametersData(
    typeof(IsEqualCondition),
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