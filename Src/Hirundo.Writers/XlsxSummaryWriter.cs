using ClosedXML.Excel;
using Hirundo.Commons.Models;

namespace Hirundo.Writers;
public class XlsxSummaryWriter(StreamWriter stream, CancellationToken? cancellationToken = null) : ISummaryWriter, IDisposable, IAsyncDisposable
{
    private readonly StreamWriter _streamWriter = stream;
    private readonly CancellationToken? _cancellationToken = cancellationToken;

    public bool IncludeExplanation { get; set; } = false;
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;

    private static readonly string[] separator = ["\r\n", "\n"];

    public void Write(ReturningSpecimensResults results)
    {
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

        if (results.Results.Count == 0)
        {
            summary.Cell(startingRow, 1).Value = "No results to display.";
        }
        else
        {
            var headers = results.Results[0].Headers;
            var values = results.Results.Select(r => r.Values).ToArray();

            for (var i = 0; i < headers.Count; i++)
            {
                _cancellationToken?.ThrowIfCancellationRequested();

                var headerCell = summary.Cell(startingRow, i + 1);
                headerCell.Value = headers[i];
                headerCell.Style.Font.Bold = true;
            }

            for (var i = 0; i < values.Length; i++)
            {
                for (var j = 0; j < headers.Count; j++)
                {
                    summary.Cell(i + startingRow + 1, j + 1).Value = GetCellValue(values[i][j]);
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

    public async ValueTask DisposeAsync()
    {
        await _streamWriter.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        _streamWriter.Dispose();
        GC.SuppressFinalize(this);
    }

    ~XlsxSummaryWriter()
    {
        Dispose();
    }
}
