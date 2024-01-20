using System.Globalization;
using System.Runtime.Serialization;
using Hirundo.Serialization.Json;
using Newtonsoft.Json;
using Serilog;

namespace Hirundo.App.CLI;

/// <summary>
///     Przykładowa aplikacja konsolowa, która wykorzystuje bibliotekę Hirundo.
///     Program pobiera dane z dwóch tabel w bazie danych Access, przetwarza je i zapisuje wyniki do pliku CSV.
/// </summary>
internal sealed class Program
{
    private static readonly HirundoApp app = new();

    private static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
            .CreateLogger();

        var appConfig = GetConfig();

        app.Run(appConfig);
    }

    private static ApplicationConfig GetConfig()
    {
        var converter = new HirundoJsonConverter();
        var jsonConfig = File.ReadAllText("appsettings.json");
        var appConfig = JsonConvert.DeserializeObject<ApplicationConfig>(jsonConfig, converter) ?? throw new SerializationException("Błąd parsowania konfiguracji.");
        return appConfig;
    }
}