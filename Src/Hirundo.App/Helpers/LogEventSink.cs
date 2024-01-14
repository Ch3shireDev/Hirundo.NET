using System.Globalization;
using System.Windows;
using Serilog.Core;
using Serilog.Events;

namespace Hirundo.App.Helpers;

public class LogEventSink(ICollection<LogEvent> items) : ILogEventSink
{
    public void Emit(LogEvent logEvent)
    {
        ArgumentNullException.ThrowIfNull(logEvent, nameof(logEvent));
        var message = logEvent.RenderMessage(CultureInfo.InvariantCulture);
        Application.Current.Dispatcher.Invoke(() => { items.Add(logEvent); });
    }
}