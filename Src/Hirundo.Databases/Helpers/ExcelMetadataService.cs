using ClosedXML.Excel;
using Hirundo.Commons.Models;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Hirundo.Databases.Helpers;

public partial class ExcelMetadataService : IExcelMetadataService
{
    public IList<ColumnParameters> GetColumns(string path)
    {
        using var workbook = new XLWorkbook(path);

        if (IsMetadataStoredInComments(workbook, out var metadata)) return metadata;

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

    private static bool IsMetadataStoredInComments(XLWorkbook workbook, out IList<ColumnParameters> columnParameters)
    {
        try
        {
            var comments = workbook.Properties.Comments;

            if (string.IsNullOrWhiteSpace(comments))
            {
                columnParameters = [];
                return false;
            }

            var data = JsonSerializer.Deserialize<MetdataColumns>(comments);

            if (data == null)
            {
                columnParameters = [];
                return false;
            }

            columnParameters = data.Headers.Zip(data.Types, (header, type) => new ColumnParameters { DatabaseColumn = header, ValueName = header, DataType = type }).ToList();

            return true;
        }
        catch (Exception)
        {
            columnParameters = [];
            return false;
        }
    }

    private class MetdataColumns
    {
        public string[] Headers { get; set; } = [];
        public DataType[] Types { get; set; } = [];
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
        return IsNumericRegex().IsMatch(value);
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

    [GeneratedRegex(@"^\d+\.\d+$")]
    private static partial Regex IsNumericRegex();
}