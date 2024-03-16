using Hirundo.Commons;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.IsNotEqual;

[ParametersData(
    typeof(IsNotEqualCondition),
    typeof(IsNotEqualModel),
    typeof(IsNotEqualView),
    "Czy wartość nie jest równa?",
    "Warunek porównujący pole danych z podaną wartością."
)]
public class IsNotEqualViewModel(IsNotEqualModel model) : ParametersViewModel(model)
{
    public IsNotEqualModel Model => model;

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