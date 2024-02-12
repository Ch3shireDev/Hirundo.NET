namespace Hirundo.Writers.Summary;

public interface ISummaryWriterBuilder
{
    ISummaryWriterBuilder WithCsvSummaryWriterParameters(CsvSummaryWriterParameters parameters);
    ISummaryWriterBuilder WithWriterParameters(IWriterParameters resultsWriter);
    ISummaryWriter Build();
}