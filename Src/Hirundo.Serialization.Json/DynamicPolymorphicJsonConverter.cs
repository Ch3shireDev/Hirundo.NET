using System.Reflection;
using Hirundo.Commons;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Hirundo.Serialization.Json;

internal sealed class DynamicPolymorphicJsonConverter(Type type, params JsonConverter[] converters) : JsonConverter
{
    private readonly JsonSerializerSettings _settings = new()
    {
        TypeNameHandling = TypeNameHandling.None,
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.Indented,
        Converters = [new StringEnumConverter(), ..converters]
    };

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (type.IsInstanceOfType(value))
        {
            var json = JsonConvert.SerializeObject(value, _settings);
            var jobject = JObject.Parse(json);
            jobject.AddFirst(new JProperty("Type", GetType(value)));
            jobject.WriteTo(writer);
        }
    }

    public static string GetType(object filter)
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

        foreach (var type2 in Assembly.GetAssembly(type)?.GetTypes() ?? [])
        {
            if (!type2.GetInterfaces().Contains(type)) continue;

            if (type2.GetCustomAttribute<TypeDescriptionAttribute>() is not { } polymorphicAttribute) continue;

            if (polymorphicAttribute.Type == typeName)
            {
                return JsonConvert.DeserializeObject(jobject.ToString(), type2, _settings);
            }
        }

        throw new ArgumentException($"Unknown filter type: {typeName}");
    }

    public override bool CanConvert(Type objectType)
    {
        return type.IsAssignableFrom(objectType);
    }
}