using System.Windows.Input;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsInTimeBlock;

[ParametersData(
    typeof(IsInTimeBlockCondition),
    typeof(IsInTimeBlockModel),
    typeof(IsInTimeBlockView),
    "Czy pole danych jest w przedziale czasowym?",
    "Warunek sprawdzający godziny złapania osobnika."
)]
public class IsInTimeBlockViewModel(IsInTimeBlockModel model) : ParametersViewModel, IRemovable
{
    public IDataLabelRepository Repository => model.Repository;

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

    public ICommand RemoveCommand => new RelayCommand(Remove);

    public event EventHandler<ParametersEventArgs>? Removed;

    public void Remove()
    {
        Removed?.Invoke(this, new ParametersEventArgs(model.Condition));
    }
}