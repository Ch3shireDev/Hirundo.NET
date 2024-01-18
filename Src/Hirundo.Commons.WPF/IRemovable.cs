using System.Windows.Input;

namespace Hirundo.Commons.WPF;

public interface IRemovable
{
    ICommand RemoveCommand { get; }
    public event EventHandler<ParametersEventArgs>? Removed;
}