using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Writers.WPF.XlsxWriter;
public class XlsxWriterModel(XlsxSummaryWriterParameters parameters, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository) : ParametersModel(parameters, labelsRepository, speciesRepository)
{
    private XlsxSummaryWriterParameters XlsxParameters { get; } = parameters;

    public string Title
    {
        get => XlsxParameters.SpreadsheetTitle;
        set => XlsxParameters.SpreadsheetTitle = value;
    }
    public string Subtitle
    {
        get => XlsxParameters.SpreadsheetSubtitle;
        set => XlsxParameters.SpreadsheetSubtitle = value;
    }
    public bool IncludeExplanation
    {
        get => XlsxParameters.IncludeExplanation;
        set => XlsxParameters.IncludeExplanation = value;
    }
}
