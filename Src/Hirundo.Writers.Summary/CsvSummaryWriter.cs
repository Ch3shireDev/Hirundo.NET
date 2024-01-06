using System.Globalization;
using CsvHelper;
using Hirundo.Commons;

namespace Hirundo.Writers.Summary;

public class CsvSummaryWriter(StreamWriter streamWriter) : ISummaryWriter, IDisposable, IAsyncDisposable
{
    public async ValueTask DisposeAsync()
    {
        await streamWriter.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        streamWriter.Dispose();
        GC.SuppressFinalize(this);
    }

    public void WriteSummary(IEnumerable<ReturningSpecimenSummary> summary)
    {
        using var csvHelper = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        csvHelper.WriteRecords(summary);
    }
}