using ClosedXML.Excel;

namespace Hirundo.Writers;

public class XlsxBuilder
{
    private static readonly string[] separator = ["" + Environment.NewLine, "\n"];
    private string Title { get; set; } = "";
    private string Subtitle { get; set; } = "";
    private string[] Headers { get; set; } = [];
    private object?[][] Values { get; set; } = [];
    private string Explanation { get; set; } = "";
    private string WorksheetName { get; set; } = "Summary";
    private string ExplanationName { get; set; } = "Explanation";

    public XlsxBuilder WithTitle(string title)
    {
        Title = title;
        return this;
    }

    public XlsxBuilder WithSubtitle(string subtitle)
    {
        Subtitle = subtitle;
        return this;
    }

    public XlsxBuilder WithHeaders(IList<string> headers)
    {
        Headers = [.. headers];
        return this;
    }

    public XlsxBuilder WithValues(IEnumerable<object?[]> values)
    {
        Values = [.. values];
        return this;
    }

    public XlsxBuilder WithWorksheetName(string worksheetName)
    {
        WorksheetName = worksheetName;
        return this;
    }

    public XlsxBuilder WithExplanation(string explanation)
    {
        Explanation = explanation;
        return this;
    }

    public XlsxBuilder WithExplanationName(string explanationName)
    {
        ExplanationName = explanationName;
        return this;
    }

    public XLWorkbook Build()
    {
        var workbook = new XLWorkbook();

        var firstWorksheet = workbook.AddWorksheet(WorksheetName);

        var startingRow = 1;

        if (!string.IsNullOrWhiteSpace(Title))
        {
            var titleCell = firstWorksheet.Cell(startingRow, 1);
            titleCell.Value = Title;
            titleCell.Style.Font.Bold = true;

            MergeWholeRow(startingRow, firstWorksheet, Headers);

            startingRow += 1;
        }

        if (!string.IsNullOrWhiteSpace(Subtitle))
        {
            var subtitleCell = firstWorksheet.Cell(startingRow, 1);
            subtitleCell.Value = Subtitle;

            MergeWholeRow(startingRow, firstWorksheet, Headers);

            startingRow += 1;
        }

        if (startingRow != 1) startingRow += 1;

        for (var i = 0; i < Headers.Length; i++)
        {
            var headerCell = firstWorksheet.Cell(startingRow, i + 1);
            headerCell.Value = Headers[i];
            headerCell.Style.Font.Bold = true;
        }

        for (var i = 0; i < Values.Length; i++)
        {
            var values = Values[i];

            for (var j = 0; j < Headers.Length; j++)
            {
                firstWorksheet.Cell(i + startingRow + 1, j + 1).Value = GetCellValue(values[j]);
            }
        }

        AutoFitColumns(firstWorksheet);

        if (!string.IsNullOrWhiteSpace(Explanation))
        {
            var secondWorksheet = workbook.AddWorksheet(ExplanationName);

            var lines = Explanation.Split(separator, StringSplitOptions.None);

            for (var i = 0; i < lines.Length; i++)
            {
                secondWorksheet.Cell(i + 1, 1).Value = lines[i];
            }

            AutoFitColumns(secondWorksheet);
        }

        return workbook;
    }

    private static void MergeWholeRow(int startingRow, IXLWorksheet summary, string[] headers)
    {
        summary.Range(startingRow, 1, startingRow, headers.Length).Merge();
    }

    private static void AutoFitColumns(IXLWorksheet summary)
    {
        summary.Columns().AdjustToContents();
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
}
