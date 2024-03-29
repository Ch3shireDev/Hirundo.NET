using Hirundo.Writers.Summary;

namespace Hirundo.Writers.WPF.CsvWriter;

public class CsvWriterModel(CsvSummaryWriterParameters parameters)
{
    public CsvSummaryWriterParameters Parameters { get; set; } = parameters;

    public string Path { get => Parameters.Path; set => Parameters.Path = value; }
}