using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;

namespace Hirundo.Writers.WPF;

public class WriterViewModel(WriterModel model, Func<Task> runTask) : ViewModelBase
{
    public DataWriterViewModel DataWriterViewModel { get; } = DataWriterViewModelFactory.Create(model.SummaryParameters.Writer);

    public ICommand SaveDataCommand => new AsyncRelayCommand(SaveData);

    private async Task SaveData()
    {
        await runTask();
    }
}