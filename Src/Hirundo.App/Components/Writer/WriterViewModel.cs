using System.Windows.Input;
using Hirundo.App.Helpers;

namespace Hirundo.App.Components.Writer;

public class WriterViewModel(WriterModel model, Func<Task> runTask) : ViewModelBase
{
    public ICommand SaveDataCommand => new AsyncRelayCommand(SaveData);

    private async Task SaveData()
    {
        await runTask();
    }
}