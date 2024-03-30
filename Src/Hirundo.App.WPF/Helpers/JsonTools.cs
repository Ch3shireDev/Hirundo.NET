using Hirundo.Serialization.Json;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Hirundo.App.WPF.Helpers;

public static class JsonTools
{
    private static readonly JsonSerializerSettings settings = new()
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        Converters = [new HirundoJsonConverter()]
    };

    public static string Serialize(ApplicationParameters configuration)
    {
        return JsonConvert.SerializeObject(configuration, settings);
    }

    public static ApplicationParameters Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<ApplicationParameters>(json, settings) ?? throw new SerializationException("Błąd parsowania konfiguracji.");
    }
}