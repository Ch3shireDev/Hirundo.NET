using System.Globalization;
using System.Windows.Data;

namespace Hirundo.Commons.WPF.Helpers;

public class MonthConverter : IValueConverter
{
    private static readonly List<string> Months =
    [
        "Styczeń",
        "Luty",
        "Marzec",
        "Kwiecień",
        "Maj",
        "Czerwiec",
        "Lipiec",
        "Sierpień",
        "Wrzesień",
        "Październik",
        "Listopad",
        "Grudzień"
    ];

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not int month) return string.Empty;

        return month is > 0 and <= 12 ? Months[month - 1] : string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string month) return 0;

        if (Months.Contains(month))

        {
            return Months.IndexOf(month) + 1;
        }

        return -1;
    }
}