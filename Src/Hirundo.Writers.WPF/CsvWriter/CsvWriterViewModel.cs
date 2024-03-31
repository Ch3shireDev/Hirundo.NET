using Hirundo.Commons.WPF;

namespace Hirundo.Writers.WPF.CsvWriter;

[ParametersData(
    typeof(CsvSummaryWriterParameters),
    typeof(CsvWriterModel),
    typeof(CsvWriterView),
    "Zapis wyników do pliku .csv",
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