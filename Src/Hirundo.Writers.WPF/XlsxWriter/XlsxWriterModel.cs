using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Writers.WPF.XlsxWriter;
public class XlsxWriterModel(XlsxSummaryWriterParameters parameters, IDataLabelRepository repository) : ParametersModel(parameters, repository)
{
    private XlsxSummaryWriterParameters xlsxParameters { get; } = parameters;

    public string Title
    {
        get => xlsxParameters.SpreadsheetTitle;
        set => xlsxParameters.SpreadsheetTitle = value;
    }
    public string Subtitle
    {
        get => xlsxParameters.SpreadsheetSubtitle;
        set => xlsxParameters.SpreadsheetSubtitle = value;
    }
    public bool IncludeExplanation
    {
        get => xlsxParameters.IncludeExplanation;
        set => xlsxParameters.IncludeExplanation = value;
    }
}
