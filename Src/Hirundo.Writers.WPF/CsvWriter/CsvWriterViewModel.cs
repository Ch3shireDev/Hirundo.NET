using Hirundo.Commons.WPF;
using Hirundo.Writers.Summary;

namespace Hirundo.Writers.WPF.CsvWriter;

[ParametersData(
    typeof(CsvSummaryWriterParameters),
    typeof(CsvWriterModel),
    typeof(CsvWriterView),
    "Zapis do pliku .csv",
    "Zapisuje wyniki do standardowego formatu pliku .csv."
    )]
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