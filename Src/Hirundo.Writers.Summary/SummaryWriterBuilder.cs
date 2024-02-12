namespace Hirundo.Writers.Summary;

/// <summary>
///     Budowniczy dla <see cref="ISummaryWriter" />.
/// </summary>
public class SummaryWriterBuilder : ISummaryWriterBuilder
{
    private string _filename = null!;

    public ISummaryWriterBuilder WithCsvSummaryWriterParameters(CsvSummaryWriterParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        _filename = parameters.Path;
        return this;
    }

    public ISummaryWriterBuilder WithWriterParameters(IWriterParameters resultsWriter)
    {
        return resultsWriter switch
        {
            CsvSummaryWriterParameters csvSummaryWriterParameters => WithCsvSummaryWriterParameters(csvSummaryWriterParameters),
            _ => throw new ArgumentException($"Unknown writer type: {resultsWriter.GetType().Name}")
        };
    }

    public ISummaryWriter Build()
    {
        var streamWriter = new StreamWriter(_filename);
        var resultsWriter = new CsvSummaryWriter(streamWriter);
        return resultsWriter;
    }
}