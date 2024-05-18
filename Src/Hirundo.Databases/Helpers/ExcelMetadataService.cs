using ClosedXML.Excel;
using Hirundo.Commons.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Hirundo.Databases.Helpers;



public class ExcelMetadataService : IExcelMetadataService
{
    public IList<ColumnParameters> GetColumns(string path)
    {
        using var workbook = new XLWorkbook(path);
        IXLWorksheet worksheet = workbook.Worksheet(1);

        var headers = GetHeaders(worksheet);
        var columnTypes = GetTypes(worksheet);

        var columnParameters = headers.Select(GetColumnParameters).ToList();

        return columnParameters.Zip(columnTypes, (header, type) =>
        {
            header.DataType = type;
            return header;
        }).ToList();
    }

    private static List<string> GetHeaders(IXLWorksheet worksheet)
    {
        var headerRow = worksheet.Row(1);

        return headerRow.CellsUsed().Select(GetCellValue).ToList();
    }

    private static string GetCellValue(IXLCell cell)
    {
        return cell?.Value.ToString(CultureInfo.InvariantCulture) ?? "";
    }


    private static DataType[] GetTypes(IXLWorksheet worksheet)
    {
        var valuesRow = worksheet.Row(2);

        return valuesRow.CellsUsed().Select(cell =>
        {
            if (cell.DataType == XLDataType.DateTime)
            {
                return DataType.Date;
            }
            else if (cell.DataType == XLDataType.Number)
            {
                if (IsNumeric(cell))
                {
                    return DataType.Numeric;
                }
                else
                {
                    return DataType.Number;
                }
            }
            else if (cell.DataType == XLDataType.Text)
            {
                return DataType.Text;
            }
            else
            {
                return DataType.Undefined;
            }
        }).ToArray();
    }

    private static bool IsNumeric(IXLCell cell)
    {
        var value = cell.Value.ToString(CultureInfo.InvariantCulture);
        return Regex.IsMatch(value, @"^\d+\.\d+$");
    }

    private static List<string> GetHeaders(string path)
    {
        using var workbook = new XLWorkbook(path);
        var worksheet = workbook.Worksheet(1);

        return GetHeaders(worksheet);
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