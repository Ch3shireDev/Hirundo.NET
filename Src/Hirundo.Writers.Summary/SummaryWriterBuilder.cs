
using Serilog;

namespace Hirundo.Writers.Summary;

/// <summary>
///     Budowniczy dla <see cref="ISummaryWriter" />.
/// </summary>
public class SummaryWriterBuilder : ISummaryWriterBuilder
{
    private string _filename = null!;
    private CancellationToken? _cancellationToken;


    public ISummaryWriterBuilder WithWriterParameters(IList<IWriterParameters> resultsWriters)
    {
        ArgumentNullException.ThrowIfNull(resultsWriters);

        foreach (var writer in resultsWriters)
        {
            WithWriterParameters(writer);
        }

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

    public ISummaryWriterBuilder WithCsvSummaryWriterParameters(CsvSummaryWriterParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        _filename = parameters.Path;
        return this;
    }
    public ISummaryWriterBuilder WithCancellationToken(CancellationToken? cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return this;
    }

    public ISummaryWriter Build()
    {
        ArgumentNullException.ThrowIfNull(_filename);
        Log.Information("Budowanie zapisywacza podsumowań do pliku: {_filename}.", _filename);
        var streamWriter = new StreamWriter(_filename);
        try
        {
            var resultsWriter = new CsvSummaryWriter(streamWriter, _cancellationToken);
            streamWriter = null;
            return resultsWriter;
        }
        catch
        {
            throw;
        }
        finally
        {
            streamWriter?.Dispose();
        }
    }

    public ISummaryWriterBuilder NewBuilder()
    {
        return new SummaryWriterBuilder();
    }
}