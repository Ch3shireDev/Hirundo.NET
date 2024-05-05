
using ClosedXML.Excel;
using System.Globalization;

namespace Hirundo.Databases.Helpers;

public class ExcelMetadataService : IExcelMetadataService
{
    public IList<ColumnParameters> GetColumns(string path)
    {
        return GetHeaders(path).Select(GetColumnParameters).ToList();
    }

    private static List<string> GetHeaders(string path)
    {
        using var workbook = new XLWorkbook(path);
        var worksheet = workbook.Worksheet(1);

        var headerRow = worksheet.Row(1);

        return headerRow.CellsUsed().Select(GetCellValue).ToList();
    }

    private static string GetCellValue(IXLCell cell)
    {
        return cell?.Value.ToString(CultureInfo.InvariantCulture) ?? "";
    }

    private static ColumnParameters GetColumnParameters(string cellValue)
    {
        return new ColumnParameters
        {
            DatabaseColumn = cellValue,
            ValueName = cellValue
        };
    }

    public IList<string> GetDistinctValues(string path, string speciesColumn)
    {
        var headers = GetHeaders(path);
        var headersIndex = headers.IndexOf(speciesColumn);

        using var workbook = new XLWorkbook(path);
        var worksheet = workbook.Worksheet(1);

        return worksheet.Column(headersIndex + 1).CellsUsed().Skip(1).Select(GetCellValue).Distinct().ToList();
    }
}
