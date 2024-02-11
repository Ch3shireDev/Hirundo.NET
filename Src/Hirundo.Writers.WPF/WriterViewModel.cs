using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Hirundo.Writers.WPF;

public class WriterViewModel(WriterModel model, Func<Task>? runTask = null) : ObservableObject
{
    public DataWriterViewModel DataWriterViewModel => DataWriterViewModelFactory.Create(model.SummaryParameters.Writer);

    public ICommand SaveDataCommand => new AsyncRelayCommand(SaveData);

    private async Task SaveData()
    {
        if (runTask == null) return;
        await runTask();
    }
}