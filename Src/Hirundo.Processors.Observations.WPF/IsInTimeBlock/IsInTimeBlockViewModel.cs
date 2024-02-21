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
public class IsInTimeBlockViewModel(IsInTimeBlockModel model) : ParametersViewModel(model), IRemovable
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
}