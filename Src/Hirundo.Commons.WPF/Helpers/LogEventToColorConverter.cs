using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Serilog.Events;

namespace Hirundo.Commons.WPF.Helpers;

public sealed class LogEventToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is LogEvent logEvent)
        {
            return logEvent.Level switch
            {
                LogEventLevel.Verbose => Brushes.Gray,
                LogEventLevel.Debug => Brushes.Gray,
                LogEventLevel.Information => Brushes.Black,
                LogEventLevel.Warning => Brushes.Orange,
                LogEventLevel.Error => Brushes.Red,
                LogEventLevel.Fatal => Brushes.Red,
                _ => Brushes.Black
            };
        }

        return Brushes.Black;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}