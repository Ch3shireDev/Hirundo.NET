using Hirundo.Commons;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.IsEqual;

[ParametersData(
    typeof(IsEqualObservationCondition),
    typeof(IsEqualModel),
    typeof(IsEqualView),
    "Czy wartość jest równa?",
    "Warunek porównujący pole danych z podaną wartością."
)]
public class IsEqualViewModel(IsEqualModel model) : ParametersViewModel(model)
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


    public DataType DataType
    {
        get => model.DataType;
        set
        {
            model.DataType = value;
            OnPropertyChanged();
        }
    }
}