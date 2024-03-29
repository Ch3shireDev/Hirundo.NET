using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hirundo.Writers.Summary;
using System.Windows.Input;

namespace Hirundo.Writers.WPF;

public class WriterViewModel(WriterModel model, Func<Task>? runTask = null) : ObservableObject
{
    public DataWriterViewModel DataWriterViewModel => DataWriterViewModelFactory.Create(model.SummaryParameters.Writers.First());

    public ICommand SaveDataCommand => new AsyncRelayCommand(SaveData);

    public IList<DataWriterViewModel> Writers { get; } = [];

    public void AddNewWriter()
    {
        Writers.Add(DataWriterViewModelFactory.Create(new CsvSummaryWriterParameters()));
    }

    private async Task SaveData()
    {
        if (runTask == null) return;
        await runTask();
    }
}