using Hirundo.Commons.WPF;

namespace Hirundo.Writers.WPF.ExplanationWriter;

[ParametersData(
    typeof(ExplanationWriterParameters),
    typeof(ExplanationWriterModel),
    typeof(ExplanationWriterView),
    "Zapis wyjaśnienia do pliku .txt",
    "Zapisuje wyjaśnienia do standardowego formatu pliku .txt."
    )]
public class ExplanationWriterViewModel(ExplanationWriterModel model) : ParametersViewModel(model)
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
