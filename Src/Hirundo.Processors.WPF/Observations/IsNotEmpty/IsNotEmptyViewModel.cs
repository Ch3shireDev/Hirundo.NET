using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;

namespace Hirundo.Processors.WPF.Observations.IsNotEmpty;

[ParametersData(
    typeof(IsNotEmptyCondition),
    typeof(IsNotEmptyModel),
    typeof(IsNotEmptyView)
)]
public class IsNotEmptyViewModel(IsNotEmptyModel model) : ParametersViewModel(model)
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
}