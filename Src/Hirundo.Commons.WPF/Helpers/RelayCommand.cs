using System.Windows.Input;

namespace Hirundo.Commons.WPF.Helpers;

public sealed class RelayCommand(Action execute, Func<bool>? canExecute = null) : ICommand
{
    public bool CanExecute(object? parameter)
    {
        if (canExecute is null)
        {
            return true;
        }

        return canExecute();
    }

    public void Execute(object? parameter)
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        execute();
    }

    public event EventHandler? CanExecuteChanged;
}