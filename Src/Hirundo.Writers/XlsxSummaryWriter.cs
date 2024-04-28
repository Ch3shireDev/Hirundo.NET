using ClosedXML.Excel;
using Hirundo.Commons.Models;

namespace Hirundo.Writers;

public class XlsxSummaryWriter(XlsxSummaryWriterParameters parameters, StreamWriter stream, CancellationToken? cancellationToken = null) : ISummaryWriter, IDisposable, IAsyncDisposable
{
    private static readonly string[] separator = ["\r\n", "\n"];
    private readonly CancellationToken? _cancellationToken = cancellationToken;
    private readonly StreamWriter _streamWriter = stream;

    public bool IncludeExplanation => parameters.IncludeExplanation;
    public string Title => parameters.SpreadsheetTitle;
    public string Subtitle => parameters.SpreadsheetSubtitle;

    public async ValueTask DisposeAsync()
    {
        await _streamWriter.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    public void Write(ReturningSpecimensResults results)
    {
        _cancellationToken?.ThrowIfCancellationRequested();

        var workbook = new XLWorkbook();
        var summary = workbook.AddWorksheet("Summary");

        var startingRow = 1;

        if (!string.IsNullOrWhiteSpace(Title))
        {
            var titleCell = summary.Cell(startingRow, 1);
            titleCell.Value = Title;
            titleCell.Style.Font.Bold = true;

            MergeWholeRow(startingRow, summary, results);

            startingRow += 1;
        }

        if (!string.IsNullOrWhiteSpace(Subtitle))
        {
            var subtitleCell = summary.Cell(startingRow, 1);
            subtitleCell.Value = Subtitle;

            MergeWholeRow(startingRow, summary, results);

            startingRow += 1;
        }

        if (startingRow != 1) startingRow += 1;

        var resultsRows = results.Results;

        if (resultsRows.Count == 0)
        {
            summary.Cell(startingRow, 1).Value = "No results to display.";
        }
        else
        {
            var headers = GetHeaders(resultsRows);

            for (var i = 0; i < headers.Length; i++)
            {
                var headerCell = summary.Cell(startingRow, i + 1);
                headerCell.Value = headers[i];
                headerCell.Style.Font.Bold = true;
            }

            for (var i = 0; i < resultsRows.Count; i++)
            {
                var resultsRow = resultsRows[i];
                var values = GetValues(resultsRow);

                for (var j = 0; j < headers.Length; j++)
                {
                    summary.Cell(i + startingRow + 1, j + 1).Value = GetCellValue(values[j]);
                }
            }

            AutoFitColumns(summary);
        }

        if (IncludeExplanation && !string.IsNullOrWhiteSpace(results.Explanation))
        {
            var worksheet = workbook.AddWorksheet("Explanation");

            var lines = results.Explanation.Split(separator, StringSplitOptions.None);

            for (var i = 0; i < lines.Length; i++)
            {
                worksheet.Cell(i + 1, 1).Value = lines[i];
            }

            AutoFitColumns(summary);
        }

        workbook.SaveAs(_streamWriter.BaseStream);
    }

    public void Dispose()
    {
        _streamWriter.Dispose();
        GC.SuppressFinalize(this);
    }

    private string[] GetHeaders(IList<ReturningSpecimenSummary> resultsRows)
    {
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

    private static void AutoFitColumns(IXLWorksheet summary)
    {
        summary.Columns().AdjustToContents();
    }

    private static void MergeWholeRow(int startingRow, IXLWorksheet summary, ReturningSpecimensResults results)
    {
        summary.Range(startingRow, 1, startingRow, GetColumnsNumber(results)).Merge();
    }

    public static int GetColumnsNumber(ReturningSpecimensResults results)
    {
        if (results.Results.Count == 0)
        {
            return 1;
        }

        return results.Results[0].Headers.Count;
    }

    private static XLCellValue GetCellValue(object? value)
    {
        var cellValue = new XLCellValue();

        cellValue = value switch
        {
            null => cellValue,
            string s => s,
            int i => i,
            double d => d,
            decimal dec => dec,
            short sh => sh,
            DateTime dt => dt,
            _ => value.ToString()
        };

        return cellValue;
    }

    ~XlsxSummaryWriter()
    {
        Dispose();
    }
}