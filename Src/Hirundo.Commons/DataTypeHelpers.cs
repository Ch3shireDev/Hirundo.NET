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

    public static object ConvertStringToDataType(string value, DataType dataType)
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

    public static T? ConvertValue<T>(object? value) where T : struct
    {
        switch (value)
        {
            case null:
            case string stringValue when string.IsNullOrEmpty(stringValue):
                return null;
            case string stringValue:
            {
                if (typeof(T) == typeof(DateTime))
                {
                    if (DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateValue))
                    {
                        return dateValue as T?;
                    }
                }
                else if (typeof(T) == typeof(decimal))
                {
                    if (decimal.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var decimalValue))
                    {
                        return decimalValue as T?;
                    }
                }

                else if (typeof(T) == typeof(int))
                {
                    if (int.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var intValue))
                    {
                        return intValue as T?;
                    }
                }
                else if (typeof(T) == typeof(long))
                {
                    if (long.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var intValue))
                    {
                        return intValue as T?;
                    }
                }

                else if (typeof(T) == typeof(double))
                {
                    if (double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var doubleValue))
                    {
                        return doubleValue as T?;
                    }
                }

                else if (typeof(T) == typeof(bool))
                {
                    if (bool.TryParse(stringValue, out var boolValue))
                    {
                        return boolValue as T?;
                    }
                }

                return null;
            }
            default:
                return Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture) as T?;
        }
    }
}