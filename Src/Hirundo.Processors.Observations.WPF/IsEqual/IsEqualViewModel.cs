using System.Windows.Input;
using Hirundo.Commons;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;

namespace Hirundo.Processors.Observations.WPF.IsEqual;

public class IsEqualViewModel(IsEqualModel model) : ParametersViewModel, IRemovable
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
    public event EventHandler<ConditionEventArgs>? Removed;

    public void Remove()
    {
        Removed?.Invoke(this, new ConditionEventArgs(model.OriginalCondition));
    }
}