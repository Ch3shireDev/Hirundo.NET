namespace Hirundo.Writers.Summary;

public interface ISummaryWriterBuilder
{
    ISummaryWriterBuilder WithCsvSummaryWriterParameters(CsvSummaryWriterParameters parameters);
    ISummaryWriterBuilder WithWriterParameters(IWriterParameters resultsWriter);
    ISummaryWriterBuilder WithCancellationToken(CancellationToken? cancellationToken);

    /// <summary>
    ///     Tworzy nowego Budowniczego tego samego typu.
    /// </summary>
    /// <returns></returns>
    ISummaryWriterBuilder NewBuilder();
    ISummaryWriter Build();
}