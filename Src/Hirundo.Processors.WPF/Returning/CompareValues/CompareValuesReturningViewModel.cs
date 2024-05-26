using Hirundo.Commons.Models;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.WPF.Returning.CompareValues;

public abstract class CompareValuesReturningViewModel<TCondition>(CompareValuesReturningModel<TCondition> model) : ParametersViewModel(model), ICompareValuesReturningViewModel
    where TCondition : CompareValuesReturningCondition
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