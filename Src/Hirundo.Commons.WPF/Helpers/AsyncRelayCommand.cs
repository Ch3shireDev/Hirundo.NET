using System.Windows.Input;

namespace Hirundo.Commons.WPF.Helpers;

public sealed class AsyncRelayCommand(Func<Task> execute, Func<bool>? canExecute = null) : ICommand
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
        Task.Run(execute);
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? CanExecuteChanged;
}