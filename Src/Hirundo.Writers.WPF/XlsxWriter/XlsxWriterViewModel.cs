using Hirundo.Commons.WPF;

namespace Hirundo.Writers.WPF.XlsxWriter;

[ParametersData(
    typeof(XlsxSummaryWriterParameters),
    typeof(XlsxWriterModel),
    typeof(XlsxWriterView)
)]
public class XlsxWriterViewModel(XlsxWriterModel model) : ParametersViewModel(model)
{
    public bool IncludeExplanation
    {
        get => model.IncludeExplanation;
        set
        {
            model.IncludeExplanation = value;
            OnPropertyChanged();
        }
    }

    public string Title
    {
        get => model.Title;
        set
        {
            model.Title = value;
            OnPropertyChanged();
        }
    }

    public string Subtitle
    {
        get => model.Subtitle;
        set
        {
            model.Subtitle = value;
            OnPropertyChanged();
        }
    }

    public override string RemoveText => "Usuń format";
}