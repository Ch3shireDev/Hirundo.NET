using System.Windows.Input;
using Hirundo.Commons;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;

namespace Hirundo.Processors.Observations.WPF.IsInTimeBlock;

public class IsInTimeBlockViewModel(IsInTimeBlockModel model) : ParametersViewModel, IRemovable
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

    public int StartHour
    {
        get => model.StartHour;
        set
        {
            model.StartHour = value;
            OnPropertyChanged();
        }
    }

    public int EndHour
    {
        get => model.EndHour;
        set
        {
            model.EndHour = value;
            OnPropertyChanged();
        }
    }

    public IList<DataLabel> Labels => [..model.GetLabels()];

    public DataLabel? SelectedLabel
    {
        get => Labels.FirstOrDefault(l => l.Name == model.ValueName);
        set
        {
            model.ValueName = value?.Name ?? string.Empty;
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