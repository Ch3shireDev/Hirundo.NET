using Serilog;

namespace Hirundo.Writers;

/// <summary>
///     Budowniczy dla <see cref="ISummaryWriter" />.
/// </summary>
public class SummaryWriterBuilder : ISummaryWriterBuilder
{
    private CancellationToken? _cancellationToken;
    private readonly IList<Func<ISummaryWriter>> writers = [];

    public ISummaryWriterBuilder NewBuilder()
    {
        return new SummaryWriterBuilder();
    }

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
            ExplanationWriterParameters explanationWriterParameters => WithExplanationWriterParameters(explanationWriterParameters),
            XlsxSummaryWriterParameters xlsxSummaryWriterParameters => WithXlsxSummaryWriterParameters(xlsxSummaryWriterParameters),
            _ => throw new ArgumentException($"Unknown writer type: {resultsWriter.GetType().Name}")
        };
    }

    public ISummaryWriterBuilder WithCsvSummaryWriterParameters(CsvSummaryWriterParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        var action = GetSummaryWriter(parameters.Path, (streamWriter, cancellationToken) => new CsvSummaryWriter(parameters, streamWriter, cancellationToken));
        writers.Add(action);
        return this;
    }

    public ISummaryWriterBuilder WithExplanationWriterParameters(ExplanationWriterParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        var action = GetSummaryWriter(parameters.Path, (streamWriter, cancellationToken) => new ExplanationWriter(streamWriter, cancellationToken));
        writers.Add(action);
        return this;
    }

    public ISummaryWriterBuilder WithXlsxSummaryWriterParameters(XlsxSummaryWriterParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        var action = GetSummaryWriter(parameters.Path, (streamWriter, cancellationToken) => new XlsxSummaryWriter(parameters, streamWriter, cancellationToken));
        writers.Add(action);
        return this;
    }

    private Func<ISummaryWriter> GetSummaryWriter(string filename, Func<StreamWriter, CancellationToken?, ISummaryWriter> build)
    {
        return () =>
        {
            Log.Information("Budowanie zapisywacza podsumowań do pliku: {filename}.", filename);
            var streamWriter = new StreamWriter(filename);
            try
            {
                var resultsWriter = build(streamWriter, _cancellationToken);
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
        };
    }

    public ISummaryWriterBuilder WithCancellationToken(CancellationToken? cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return this;
    }

    public ISummaryWriter Build()
    {
        var writers = this.writers.Select(writer => writer()).ToArray();
        return new CompositeSummaryWriter(writers);
    }

}