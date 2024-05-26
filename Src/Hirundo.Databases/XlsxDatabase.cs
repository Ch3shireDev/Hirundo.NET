using ClosedXML.Excel;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;
using System.Globalization;

namespace Hirundo.Databases;

public class XlsxDatabase(ExcelDatabaseParameters parameters, CancellationToken? token = null) : IDatabase
{
    public string Path { get; } = parameters.Path;
    public string RingIdentifier { get; } = parameters.RingIdentifier;
    public string DateIdentifier { get; } = parameters.DateIdentifier;
    public string SpeciesIdentifier { get; } = parameters.SpeciesIdentifier;

    public IEnumerable<Observation> GetObservations()
    {
        using var workbook = new XLWorkbook(Path);
        var worksheet = workbook.Worksheets.First();
        var headers = worksheet.Row(1).Cells().Select(c => c.Value.ToString(CultureInfo.InvariantCulture)).ToList();
        var types = GetTypes(parameters).ToArray();

        var ringIndex = headers.IndexOf(RingIdentifier);
        var speciesIndex = headers.IndexOf(SpeciesIdentifier);
        var dateIndex = headers.IndexOf(DateIdentifier);

        foreach (var row in worksheet.RowsUsed().Skip(1))
        {
            token?.ThrowIfCancellationRequested();

            var values = GetValues(parameters, row).ToArray();

            var ring = values[ringIndex]?.ToString() ?? "";
            var species = values[speciesIndex]?.ToString() ?? "";
            var date = values[dateIndex] as DateTime? ?? DateTime.MinValue;

            yield return new Observation
            {
                Ring = ring,
                Species = species,
                Date = date,
                Headers = headers,
                Values = values,
                Types = types,
            };
        }
    }

    private static IEnumerable<DataType> GetTypes(ExcelDatabaseParameters parameters)
    {
        return parameters.Columns.Select(c => c.DataType);
    }

    private static IEnumerable<object?> GetValues(ExcelDatabaseParameters parameters, IXLRow? row)
    {
        for (var i = 0; i < parameters.Columns.Count; i++)
        {
            var column = parameters.Columns[i];
            var cellValue = row?.Cell(i + 1)?.Value;
            if (cellValue == null)
            {
                yield return null;
                continue;
            }
            if (cellValue.Value.IsBlank)
            {
                yield return null;
                continue;
            }

            var value = Convert(cellValue.ToString(), column.DataType);
            yield return value;
        }
    }

    private static object? Convert(object? value, DataType dataType)
    {
        return DataTypeHelpers.ConvertValueToDataType(value, dataType);
    }
}
