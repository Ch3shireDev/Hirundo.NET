using System.Collections.ObjectModel;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.IsSpeciesEqual;

[ParametersData(
    typeof(IsSpeciesEqualCondition),
    typeof(IsSpeciesEqualModel),
    typeof(IsSpeciesEqualView)
)]
public class IsSpeciesEqualViewModel(IsSpeciesEqualModel model) : ParametersViewModel(model)
{
    public ObservableCollection<string> SpeciesList { get; } = [];

    public string Species
    {
        get => model.Species;
        set
        {
            model.Species = value;
            OnPropertyChanged();
        }
    }
}