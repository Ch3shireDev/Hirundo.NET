using System.Globalization;
using System.Windows.Data;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics.WPF.Helpers;

public class OperationToStringConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Type type)
        {
            return type.Name switch
            {
                nameof(AverageOperation) => "Wartość średnia + odchylenie standardowe",
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