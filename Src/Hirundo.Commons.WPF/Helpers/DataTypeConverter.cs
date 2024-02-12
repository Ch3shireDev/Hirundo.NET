using System.Globalization;
using System.Windows.Data;
using Hirundo.Databases;

namespace Hirundo.Commons.WPF.Helpers;

public class DataTypeConverter : IValueConverter
{
    private static readonly Dictionary<object, string> valueDictionary = new()
    {
        { DataValueType.Text, "Tekst" },
        { DataValueType.LongInt, "Duża liczba całkowita" },
        { DataValueType.ShortInt, "Mała liczba całkowita" },
        { DataValueType.Numeric, "Liczba zmiennoprzecinkowa" },
        { DataValueType.DateTime, "Data i czas" },
        { DataValueType.Undefined, "Nieokreślony" }
    };

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return value;

        return ConvertToString(value);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var valueStr = value?.ToString() ?? "";

        return valueDictionary.ContainsValue(valueStr) ? valueDictionary.FirstOrDefault(x => x.Value == valueStr).Key : value;
    }

    public static string ConvertToString(object? value)
    {
        if (value == null) return "";
        return valueDictionary.TryGetValue(value, out var result) ? result : value.ToString() ?? "";
    }
}