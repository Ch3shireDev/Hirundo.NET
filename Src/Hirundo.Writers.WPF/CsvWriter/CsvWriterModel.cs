using Hirundo.Writers.Summary;

namespace Hirundo.Writers.WPF.CsvWriter;

public class CsvWriterModel
{
    public CsvWriterModel(CsvSummaryWriterParameters parameters)
    {
        Parameters = parameters;
    }

    public CsvSummaryWriterParameters Parameters { get; set; }

    public string Path { get => Parameters.Path; set => Parameters.Path = value; }
}