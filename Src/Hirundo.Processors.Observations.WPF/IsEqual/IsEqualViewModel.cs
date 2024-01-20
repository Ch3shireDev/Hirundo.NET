using System.Windows.Input;
using Hirundo.Commons;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Repositories.DataLabels;

namespace Hirundo.Processors.Observations.WPF.IsEqual;

public class IsEqualViewModel(IsEqualModel model) : ParametersViewModel, IRemovable
{
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

    public IList<DataLabel> Labels => model.Labels;

    public DataLabel? SelectedLabel
    {
        get => model.SelectedLabel;
        set
        {
            model.SelectedLabel = value;
            OnPropertyChanged();
        }
    }

    public ICommand RemoveCommand => new RelayCommand(Remove);

    public event EventHandler<ParametersEventArgs>? Removed;

    public void Remove()
    {
        Removed?.Invoke(this, new ParametersEventArgs(model.OriginalCondition));
    }
}