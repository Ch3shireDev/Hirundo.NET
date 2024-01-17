using System.Globalization;
using System.Windows.Data;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.Helpers;

internal class ReturningFilterToStringConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Type type)
        {
            return type.Name switch
            {
                nameof(ReturnsAfterTimePeriodFilter) => "Czy powraca po ustalonym czasie?",
                nameof(ReturnsNotEarlierThanGivenDateNextYearFilter) => "Czy powraca nie wcześniej niż przed ustaloną datą kolejnego roku?",
                _ => throw new NotImplementedException()
            };
        }

        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}