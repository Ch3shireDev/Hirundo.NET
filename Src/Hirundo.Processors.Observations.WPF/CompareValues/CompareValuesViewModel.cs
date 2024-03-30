using Hirundo.Commons.Models;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.CompareValues;
public class CompareValuesViewModel<TCondition>(CompareValuesModel<TCondition> model) : ParametersViewModel(model), ICompareValuesViewModel
    where TCondition : ICompareValueCondition
{
    public virtual string ValueDescription => "Wartość do porównania";

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
