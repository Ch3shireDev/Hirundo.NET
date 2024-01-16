namespace Hirundo.Writers.Summary;

public class SummaryParameters
{
    public IWriterParameters Writer { get; set; } = new CsvSummaryWriterParameters();
}