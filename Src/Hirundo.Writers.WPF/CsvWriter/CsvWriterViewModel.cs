using Hirundo.Commons.WPF;

namespace Hirundo.Writers.WPF.CsvWriter;

[ParametersData(typeof(CsvSummaryWriterParameters), typeof(CsvWriterModel), typeof(CsvWriterView))]
public class CsvWriterViewModel(CsvWriterModel model) : ParametersViewModel(model)
{
    public string Path
    {
        get => model.Path;
        set
        {
            model.Path = value;
            OnPropertyChanged();
        }
    }

    public override string RemoveText => "Usuń format";
}