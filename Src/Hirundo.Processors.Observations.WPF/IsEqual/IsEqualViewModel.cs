using System.Windows.Input;
using Hirundo.Commons;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsEqual;

internal class IsEqualViewModel(IsEqualModel model) : ConditionViewModel, IRemovable<IObservationFilter>
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
        get => model.ValueType;
        set
        {
            model.ValueType = value;
            OnPropertyChanged();
        }
    }

    public ICommand RemoveCommand => new RelayCommand(Remove);
    public event EventHandler<IObservationFilter>? Removed;

    public void Remove()
    {
        Removed?.Invoke(this, model.OriginalCondition);
    }

    public override string ToString()
    {
        return "Czy jest równy?";
    }
}