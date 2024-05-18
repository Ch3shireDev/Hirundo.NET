using Serilog;
using System.Windows;
using System.Windows.Input;

namespace Hirundo.Commons.WPF.Helpers;

public static class WindowHelpers
{
    public static void SetMouseCursor(Cursor? cursor = null)
    {
        try
        {
            Application.Current?.Dispatcher?.Invoke(() => { Mouse.OverrideCursor = cursor; });
        }
        catch (Exception e)
        {
            Log.Error($"Błąd ustawiania kursora. Informacja o błędzie: {e.Message}", e);
            throw;
        }
    }
}
