using System.Globalization;
using System.Windows.Data;

namespace Hirundo.Commons.WPF.Helpers;

public class MonthConverter : IValueConverter
{
    private static readonly List<string> _months =
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
    public IList<int> Months { get; } = Enumerable.Range(1, 12).ToList();
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not int month) return string.Empty;

        return month is > 0 and <= 12 ? _months[month - 1] : string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int) return value;
        if (value is not string month) return 0;

        if (_months.Contains(month))

        {
            return _months.IndexOf(month) + 1;
        }

        return -1;
    }
}