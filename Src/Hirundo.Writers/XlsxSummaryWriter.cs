using Hirundo.Commons.Models;

namespace Hirundo.Writers;

public class XlsxSummaryWriter(XlsxSummaryWriterParameters parameters, StreamWriter stream, CancellationToken? cancellationToken = null) : ISummaryWriter, IDisposable, IAsyncDisposable
{
    private readonly CancellationToken? _cancellationToken = cancellationToken;
    private readonly StreamWriter _streamWriter = stream;

    public bool IncludeExplanation => parameters.IncludeExplanation;
    public string Title => parameters.SpreadsheetTitle;
    public string Subtitle => parameters.SpreadsheetSubtitle;

    public void Write(ReturningSpecimensResults results)
    {
        _cancellationToken?.ThrowIfCancellationRequested();

        var headers = GetHeaders(results.Results);
        var values = results.Results.Select(GetValues);

        var explanation = IncludeExplanation ? results.Explanation : "";

        var workbook = new XlsxBuilder()
                .WithTitle(Title)
                .WithSubtitle(Subtitle)
                .WithHeaders(headers)
                .WithValues(values)
                .WithExplanation(explanation)
                .Build();

        workbook.SaveAs(_streamWriter.BaseStream);
    }

    private string[] GetHeaders(IList<ReturningSpecimenSummary> resultsRows)
    {
        if (resultsRows.Count == 0)
        {
            return ["No results to display."];
        }

        return
        [
            parameters.RingHeaderName,
            parameters.DateFirstSeenHeaderName,
            parameters.DateLastSeenHeaderName,
            .. resultsRows[0].Headers
        ];
    }

    private static object?[] GetValues(ReturningSpecimenSummary resultsRow)
    {
        return [resultsRow.Ring, resultsRow.DateFirstSeen, resultsRow.DateLastSeen, .. resultsRow.Values];
    }

    ~XlsxSummaryWriter()
    {
        Dispose();
    }

    public void Dispose()
    {
        _streamWriter.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await _streamWriter.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }
}