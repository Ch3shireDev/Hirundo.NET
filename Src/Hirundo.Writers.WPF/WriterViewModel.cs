using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons.WPF;
using Hirundo.Writers.Summary;
using System.Windows.Input;

namespace Hirundo.Writers.WPF;

public class WriterViewModel(WritersModel model, Func<Task>? runTask = null) : ObservableObject
{
    public ParametersViewModel DataWriterViewModel => WritersParametersFactory.Create(model.SummaryParameters.Writers.First());

    public ICommand SaveDataCommand => new AsyncRelayCommand(SaveData);

    public IList<ParametersViewModel> Writers { get; } = [];

    public void AddNewWriter()
    {
        Writers.Add(WritersParametersFactory.Create(new CsvSummaryWriterParameters()));
    }

    private async Task SaveData()
    {
        if (runTask == null) return;
        await runTask();
    }
}