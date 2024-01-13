using System.Globalization;
using System.Windows.Data;
using Serilog.Events;

namespace Hirundo.App.Helpers;

internal sealed class LogEventMessageConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is LogEvent logEvent)
        {
            var timestamp = logEvent.Timestamp.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
            var message = logEvent.RenderMessage(CultureInfo.InvariantCulture);
            return $"[{timestamp}] {message}";
        }

        return string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}