using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Specimens.WPF;

public class SpecimensViewModel(SpecimensModel model) : ViewModelBase
{
    public string SpecimenIdentifier
    {
        get => model.SpecimenIdentifier;
        set
        {
            model.SpecimenIdentifier = value;
            OnPropertyChanged();
        }
    }

    public bool IncludeEmptyValues
    {
        get => model.IncludeEmptyValues;
        set
        {
            model.IncludeEmptyValues = value;
            OnPropertyChanged();
        }
    }
}