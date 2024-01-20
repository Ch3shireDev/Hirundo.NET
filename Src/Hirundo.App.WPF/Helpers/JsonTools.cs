using System.Runtime.Serialization;
using Hirundo.Configuration;
using Hirundo.Serialization.Json;
using Newtonsoft.Json;

namespace Hirundo.App.WPF.Helpers;

public static class JsonTools
{
    private static readonly JsonSerializerSettings settings = new()
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        Converters = new List<JsonConverter>
        {
            new HirundoJsonConverter()
        }
    };

    public static string Serialize(ApplicationConfig configuration)
    {
        return JsonConvert.SerializeObject(configuration, settings);
    }

    public static ApplicationConfig Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<ApplicationConfig>(json, settings) ?? throw new SerializationException("Błąd parsowania konfiguracji.");
    }
}