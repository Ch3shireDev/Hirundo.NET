using System.Reflection;
using Hirundo.Commons;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Formatting = Newtonsoft.Json.Formatting;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Hirundo.Serialization.Json;

internal sealed class StaticPolymorphicJsonConverter<T> : JsonConverter where T : class
{
    private readonly JsonSerializerSettings _settings = new()
    {
        TypeNameHandling = TypeNameHandling.None,
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.Indented,
        Converters = new List<JsonConverter>
        {
            new StringEnumConverter()
        }
    };

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is T filter)
        {
            var json = JsonConvert.SerializeObject(filter, _settings);
            var jobject = JObject.Parse(json);
            jobject.AddFirst(new JProperty("Type", GetType(filter)));
            jobject.WriteTo(writer);
        }
    }

    public static string GetType(T filter)
    {
        if (filter.GetType().GetCustomAttribute<TypeDescriptionAttribute>() is { } polymorphicAttribute)
        {
            return polymorphicAttribute.Type;
        }

        throw new ArgumentException($"Unknown object type: {filter.GetType().Name}");
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jobject = JObject.Load(reader);
        var typeName = jobject["Type"]?.Value<string>();

        foreach (var type in Assembly.GetAssembly(typeof(T))?.GetTypes() ?? [])
        {
            if (!type.GetInterfaces().Contains(typeof(T))) continue;

            if (type.GetCustomAttribute<TypeDescriptionAttribute>() is not { } polymorphicAttribute) continue;

            if (polymorphicAttribute.Type == typeName)
            {
                return JsonConvert.DeserializeObject(jobject.ToString(), type, _settings);
            }
        }

        throw new ArgumentException($"Unknown object type: {typeName}");
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(T).IsAssignableFrom(objectType);
    }
}