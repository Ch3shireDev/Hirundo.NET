using CommunityToolkit.Mvvm.ComponentModel;
using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;

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

    public DataType DataType { get; set; }
    public IDataLabelRepository Repository { get; } = repository;
}