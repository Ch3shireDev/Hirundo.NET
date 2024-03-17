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

    public static object ConvertValueToDataType(object value, DataType dataType)
    {
        return dataType switch
        {
            DataType.Text => ConvertToString(value),
            DataType.Number => ConvertValue<int>(value) ?? 0,
            DataType.Numeric => ConvertValue<double>(value) ?? 0,
            DataType.Date => ConvertValue<DateTime>(value) ?? DateTime.MinValue,
            DataType.Boolean => ConvertValue<bool>(value) ?? false,
            DataType.Undefined => value,
            _ => value
        };
    }

    public static bool IsSoftEqual(object? value1, object? value2)
    {
        if (value1 is null)
        {
            return IsNullOrEmptyString(value2);
        }

        if (value2 is null)
        {
            return IsNullOrEmptyString(value1);
        }

        if (DoesEqualAsSoftNumber(value1, value2))
        {
            return true;
        }

        if (DoesEqualAsSoftDate(value1, value2))
        {
            return true;
        }

        if (value1.GetType() == value2.GetType())
        {
            return Equals(value1, value2);
        }

        var typeValue = ConvertValue(value1, value2.GetType());
        return Equals(typeValue, value2);
    }

    private static bool DoesEqualAsSoftDate(object? value1, object? value2)
    {
        if (!IsConvertableToDate(value1)) return false;
        if (!IsConvertableToDate(value2)) return false;

        var date1 = Convert.ToDateTime(value1, CultureInfo.InvariantCulture);
        var date2 = Convert.ToDateTime(value2, CultureInfo.InvariantCulture);

        return date1.Equals(date2);
    }

    private static bool DoesEqualAsSoftNumber(object? value1, object? value2)
    {
        if (!IsConvertableToNumber(value1)) return false;
        if (!IsConvertableToNumber(value2)) return false;

        var number1 = Convert.ToDouble(value1, CultureInfo.InvariantCulture);
        var number2 = Convert.ToDouble(value2, CultureInfo.InvariantCulture);

        return Equals(number1, number2);
    }

    public static bool IsGreaterThanNumeric(object? value1, object? value2)
    {
        if (!IsConvertableToNumber(value1)) return false;
        if (!IsConvertableToNumber(value2)) return false;

        var number1 = Convert.ToDouble(value1, CultureInfo.InvariantCulture);
        var number2 = Convert.ToDouble(value2, CultureInfo.InvariantCulture);

        return number1 > number2;
    }

    public static bool IsLowerThanNumeric(object? value1, object? value2)
    {
        if (!IsConvertableToNumber(value1)) return false;
        if (!IsConvertableToNumber(value2)) return false;

        var number1 = Convert.ToDouble(value1, CultureInfo.InvariantCulture);
        var number2 = Convert.ToDouble(value2, CultureInfo.InvariantCulture);

        return number1 < number2;
    }

    public static bool IsGreaterThanDate(object? value1, object? value2)
    {
        if (!IsConvertableToDate(value1)) return false;
        if (!IsConvertableToDate(value2)) return false;

        var date1 = Convert.ToDateTime(value1, CultureInfo.InvariantCulture);
        var date2 = Convert.ToDateTime(value2, CultureInfo.InvariantCulture);

        return date1 > date2;
    }
    public static bool IsLowerThanDate(object? value1, object? value2)
    {
        if (!IsConvertableToDate(value1)) return false;
        if (!IsConvertableToDate(value2)) return false;

        var date1 = Convert.ToDateTime(value1, CultureInfo.InvariantCulture);
        var date2 = Convert.ToDateTime(value2, CultureInfo.InvariantCulture);

        return date1 < date2;
    }

    public static bool IsConvertableToDate(object? value)
    {
        if (value == null) return false;
        if (value is DateTime) return true;
        if (value is string valueStr)
        {
            return DateTime.TryParse(valueStr, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

        }
        return false;
    }

    private static bool IsNullOrEmptyString(object? value)
    {
        if (value == null) return true;
        return value is string str && string.IsNullOrEmpty(str);
    }

    public static bool IsConvertableToNumber(object? value)
    {
        if (value == null) return false;
        if (value is int || value is double || value is float || value is decimal) return true;
        if (value is string valueStr)
        {
            return double.TryParse(valueStr, out _);
        }
        return false;
    }

    public static object? ConvertValue(object? value, Type type)
    {
        switch (value)
        {
            case null:
            case string stringValue when string.IsNullOrEmpty(stringValue):
                return null;
            case string stringValue:
                {
                    if (type == typeof(string))
                    {
                        return stringValue;
                    }
                    else if (type == typeof(DateTime))
                    {
                        if (DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateValue))
                        {
                            return dateValue;
                        }
                    }
                    else if (type == typeof(decimal))
                    {
                        if (decimal.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var decimalValue))
                        {
                            return decimalValue;
                        }
                    }

                    else if (type == typeof(int))
                    {
                        if (int.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var intValue))
                        {
                            return intValue;
                        }
                    }
                    else if (type == typeof(long))
                    {
                        if (long.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var intValue))
                        {
                            return intValue;
                        }
                    }

                    else if (type == typeof(double))
                    {
                        if (double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var doubleValue))
                        {
                            return doubleValue;
                        }
                    }

                    else if (type == typeof(bool))
                    {
                        if (bool.TryParse(stringValue, out var boolValue))
                        {
                            return boolValue;
                        }
                    }

                    return null;
                }
            default:
                return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }

    }

    public static T? ConvertValue<T>(object? value) where T : struct
    {
        return ConvertValue(value, typeof(T)) as T?;
    }
}