using Hirundo.Commons.WPF;
using System.Collections.ObjectModel;

namespace Hirundo.Processors.Observations.WPF.IsSpeciesEqual;
[ParametersData(
    typeof(IsSpeciesEqualCondition),
    typeof(IsSpeciesEqualModel),
    typeof(IsSpeciesEqualView),
    "Czy jest danego gatunku?",
    "Warunek sprawdzający, zaobserwowany osobnik należy do wskazanego gatunku."
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
