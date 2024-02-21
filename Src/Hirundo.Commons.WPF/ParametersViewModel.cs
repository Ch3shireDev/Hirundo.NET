using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons.Repositories.Labels;
using System.Windows.Input;

namespace Hirundo.Commons.WPF;

public abstract class ParametersViewModel(ParametersModel model) : ObservableObject, IRemovable
{
    private readonly ParametersModel model = model;
    public string Type { get; set; } = null!;
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;
    public virtual string RemoveText => "Usuń warunek";

    public event EventHandler<ParametersEventArgs>? Removed;

    protected virtual void Remove(object condition)
    {
        Removed?.Invoke(this, new ParametersEventArgs(condition));
    }

    public virtual IDataLabelRepository Repository => model.Repository;
    public virtual ICommand RemoveCommand => new RelayCommand(() => Remove(model.Parameters));
}