namespace Hirundo.Writers;

public interface ISummaryWriterBuilder
{
    ISummaryWriterBuilder WithCsvSummaryWriterParameters(CsvSummaryWriterParameters parameters);
    ISummaryWriterBuilder WithWriterParameters(IWriterParameters resultsWriter);
    ISummaryWriterBuilder WithWriterParameters(IList<IWriterParameters> resultsWriters);
    ISummaryWriterBuilder WithCancellationToken(CancellationToken? cancellationToken);

    /// <summary>
    ///     Tworzy nowego Budowniczego tego samego typu.
    /// </summary>
    /// <returns></returns>
    ISummaryWriterBuilder NewBuilder();

    ISummaryWriter Build();
}