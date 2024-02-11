using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Hirundo.Commons.WPF;

public abstract class ParametersViewModel : ObservableObject, IRemovable
{
    public string Type { get; set; } = null!;
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;
    public virtual string RemoveText => "Usuń warunek";
    public abstract ICommand RemoveCommand { get; }
    public event EventHandler<ParametersEventArgs>? Removed;

    protected void Remove(object condition)
    {
        Removed?.Invoke(this, new ParametersEventArgs(condition));
    }
}