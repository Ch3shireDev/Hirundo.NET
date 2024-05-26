using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.WPF.Population.IsEqual;

[ParametersData(
    typeof(IsEqualPopulationCondition),
    typeof(IsEqualPopulationModel),
    typeof(IsEqualPopulationView)
)]
public class IsEqualPopulationViewModel(IsEqualPopulationModel model) : ParametersViewModel(model)
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
        get => model.Value;
        set
        {
            model.Value = value;
            OnPropertyChanged();
        }
    }
}