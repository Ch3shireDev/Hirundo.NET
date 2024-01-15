using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;

namespace Hirundo.Writers.WPF;

public class WriterViewModel(WriterModel model, Func<Task> runTask) : ViewModelBase
{
    public ICommand SaveDataCommand => new AsyncRelayCommand(SaveData);

    private async Task SaveData()
    {
        await runTask();
    }
}