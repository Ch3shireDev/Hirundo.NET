namespace Hirundo.Writers.Summary;

/// <summary>
///     Budowniczy dla <see cref="ISummaryWriter" />.
/// </summary>
public class SummaryWriterBuilder
{
    private string _filename = null!;

    public SummaryWriterBuilder WithFilename(string filename)
    {
        _filename = filename;
        return this;
    }

    public SummaryWriterBuilder WithCsvSummaryWriterParameters(CsvSummaryWriterParameters parameters)
    {
        _filename = parameters.SummaryFilepath;
        return this;
    }

    public ISummaryWriter Build()
    {
        var streamWriter = new StreamWriter(_filename);
        var resultsWriter = new CsvSummaryWriter(streamWriter);
        return resultsWriter;
    }
}