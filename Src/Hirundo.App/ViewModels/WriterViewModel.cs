using System.Windows.Input;
using Hirundo.App.Helpers;
using Hirundo.App.Models;

namespace Hirundo.App.ViewModels;

public class WriterViewModel(WriterModel model, Func<Task> runTask) : ViewModelBase
{
    public ICommand SaveDataCommand => new AsyncRelayCommand(SaveData);

    private async Task SaveData()
    {
        await runTask();
    }
}