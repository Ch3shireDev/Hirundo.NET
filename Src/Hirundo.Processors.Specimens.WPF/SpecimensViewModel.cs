using CommunityToolkit.Mvvm.ComponentModel;
using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;

namespace Hirundo.Processors.Specimens.WPF;

public class SpecimensViewModel(SpecimensModel model, IDataLabelRepository repository) : ObservableObject
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

    public DataType DataType { get; set; }
    public IDataLabelRepository Repository { get; } = repository;
}