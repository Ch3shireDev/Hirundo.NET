using System.Windows.Input;

namespace Hirundo.Commons.WPF.Helpers;

public sealed class AsyncRelayCommand(Func<Task> execute, Func<Task<bool>>? canExecute = null) : ICommand
{
    public bool CanExecute(object? parameter)
    {
        if (canExecute is null)
        {
            return true;
        }

        if (canExecute().Result)
        {
            return true;
        }

        return false;
    }

    public void Execute(object? parameter)
    {
        Task.Run(execute);
    }

    public event EventHandler? CanExecuteChanged;
}