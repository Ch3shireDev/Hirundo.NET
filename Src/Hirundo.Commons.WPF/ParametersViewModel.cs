using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons.Repositories;

namespace Hirundo.Commons.WPF;

public abstract class ParametersViewModel(ParametersModel model) : ObservableObject, IRemovable
{
    private readonly ParametersModel model = model;
    public string Type { get; set; } = null!;
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;
    public virtual string RemoveText => "Usuń warunek";

    public virtual ILabelsRepository LabelsRepository => model.LabelsRepository;
    public virtual ISpeciesRepository SpeciesRepository => model.SpeciesRepository;

    public event EventHandler<ParametersEventArgs>? Removed;
    public virtual ICommand RemoveCommand => new RelayCommand(() => Remove(model.Parameters));

    protected virtual void Remove(object condition)
    {
        Removed?.Invoke(this, new ParametersEventArgs(condition));
    }
}