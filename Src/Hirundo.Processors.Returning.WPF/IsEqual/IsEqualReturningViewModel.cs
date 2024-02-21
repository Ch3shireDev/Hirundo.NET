using Hirundo.Commons;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.IsEqual;

[ParametersData(
    typeof(IsEqualReturningCondition),
    typeof(IsEqualReturningModel),
    typeof(IsEqualReturningView),
    "Czy dane są równe?",
    "Osobnik zawiera obserwację z polem równym danej wartości."
)]
public class IsEqualReturningViewModel(IsEqualReturningModel model) : ParametersViewModel(model)
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

    public string Value
    {
        get => model.ValueStr;
        set
        {
            model.ValueStr = value;
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