using Newtonsoft.Json;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace Hirundo.Serialization.Json;

public static class JsonTools
{
    private static readonly JsonSerializerSettings settings = new()
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        Converters = [new HirundoJsonConverter()]
    };

    public static string Serialize(object configuration)
    {
        return JsonConvert.SerializeObject(configuration, settings);
    }

    public static T Deserialize<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json, settings) ?? throw new SerializationException("Błąd parsowania konfiguracji.");
    }

    public static string GetConfigHash(object config)
    {
        var converter = new HirundoJsonConverter();
        var settings = new JsonSerializerSettings();
        settings.Converters.Add(converter);
        string jsonString = JsonConvert.SerializeObject(config, settings);
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(jsonString));
        var builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2", CultureInfo.InvariantCulture));
        }
        return builder.ToString();
    }
}