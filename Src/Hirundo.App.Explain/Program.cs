using Hirundo.Commons.Helpers;
using Hirundo.Serialization.Json;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Hirundo.App.Explain;

internal class Program
{
    private static void Main(string[] args)
    {
        var config = GetConfig();
        var result = ExplainerHelpers.Explain(config);
        Console.WriteLine(result);
        File.WriteAllText("appsettings.explanation.txt", result);
    }

    private static ApplicationParameters GetConfig()
    {
        var converter = new HirundoJsonConverter();
        var jsonConfig = File.ReadAllText("appsettings.json");
        var appConfig = JsonConvert.DeserializeObject<ApplicationParameters>(jsonConfig, converter) ?? throw new SerializationException("Błąd parsowania konfiguracji.");
        return appConfig;
    }
}
