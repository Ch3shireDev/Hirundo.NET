using System.Globalization;

namespace Hirundo.Commons;

public static class DataTypeHelpers
{
    public static string GetValueToString(object? value, DataType dataType)
    {
        return dataType switch
        {
            DataType.Text => value as string ?? string.Empty,
            DataType.Number => value is int intValue ? intValue.ToString(CultureInfo.InvariantCulture) : string.Empty,
            DataType.Numeric => value is double doubleValue ? doubleValue.ToString(CultureInfo.InvariantCulture) : string.Empty,
            DataType.Date => value is DateTime dateValue ? dateValue.ToString(CultureInfo.InvariantCulture) : string.Empty,
            DataType.Boolean => value is bool boolValue ? boolValue.ToString(CultureInfo.InvariantCulture) : string.Empty,
            DataType.Undefined => ConvertToString(value),
            _ => string.Empty
        };
    }

    private static string ConvertToString(object? value)
    {
        if (value is DateTime dateValue)
        {
            return dateValue.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        if (value is decimal decimalValue)
        {
            return decimalValue.ToString(CultureInfo.InvariantCulture);
        }

        return value?.ToString() ?? string.Empty;
    }

    public static object GetValueSetValueFromString(string value, DataType dataType)
    {
        return dataType switch
        {
            DataType.Text => value,
            DataType.Number when int.TryParse(value, out var intValue) => intValue,
            DataType.Number => value,
            DataType.Numeric when double.TryParse(value, out var doubleValue) => doubleValue,
            DataType.Numeric => value,
            DataType.Date when DateTime.TryParse(value, out var dateValue) => dateValue,
            DataType.Date => value,
            DataType.Boolean when bool.TryParse(value, out var boolValue) => boolValue,
            DataType.Boolean => value,
            DataType.Undefined => value,
            _ => value
        };
    }
}