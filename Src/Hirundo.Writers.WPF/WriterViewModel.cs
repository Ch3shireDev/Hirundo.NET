using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;

namespace Hirundo.Writers.WPF;

public class WriterViewModel(WriterModel model, Func<Task>? runTask=null) : ViewModelBase
{
    public DataWriterViewModel DataWriterViewModel => DataWriterViewModelFactory.Create(model.SummaryParameters.Writer);

    public ICommand SaveDataCommand => new AsyncRelayCommand(SaveData);

    private async Task SaveData()
    {
        if (runTask == null) return;
        await runTask();
    }
}