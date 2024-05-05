
using ClosedXML.Excel;
using System.Globalization;

namespace Hirundo.Databases.Helpers;

public class ExcelMetadataService : IExcelMetadataService
{
    public IList<ColumnParameters> GetColumns(string path)
    {
        using var workbook = new XLWorkbook(path);
        var worksheet = workbook.Worksheet(1);

        var headerRow = worksheet.Row(1);

        return headerRow.CellsUsed().Select(GetColumnParameters).ToList();
    }

    private static ColumnParameters GetColumnParameters(IXLCell cell)
    {
        return new ColumnParameters
        {
            DatabaseColumn = cell?.Value.ToString(CultureInfo.InvariantCulture) ?? "",
            ValueName = cell?.Value.ToString(CultureInfo.InvariantCulture) ?? ""
        };
    }
}
