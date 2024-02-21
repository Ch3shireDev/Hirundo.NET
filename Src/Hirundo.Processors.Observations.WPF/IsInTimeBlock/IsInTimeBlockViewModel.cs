using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF.IsInTimeBlock;

[ParametersData(
    typeof(IsInTimeBlockCondition),
    typeof(IsInTimeBlockModel),
    typeof(IsInTimeBlockView),
    "Czy dane są w przedziale godzinowym?",
    "Warunek sprawdzający godziny złapania osobnika."
)]
public class IsInTimeBlockViewModel(IsInTimeBlockModel model) : ParametersViewModel, IRemovable
{
    public override IDataLabelRepository Repository => model.Repository;

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

    public override ICommand RemoveCommand => new RelayCommand(() => Remove(model.Condition));
}