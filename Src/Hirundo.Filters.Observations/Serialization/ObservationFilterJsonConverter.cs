using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Hirundo.Filters.Observations.Serialization;

public class ObservationFilterJsonConverter : JsonConverter
{
    private static readonly JsonSerializerSettings _settings = new()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.Indented,
        Converters = new List<JsonConverter>
        {
            new StringEnumConverter()
        }
    };

    public static string SerializeFilterWithType(IObservationFilter filter, string type)
    {
        var json = JsonConvert.SerializeObject(filter, _settings);
        var jobject = JObject.Parse(json);
        jobject.AddFirst(new JProperty("Type", type));
        return jobject.ToString();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if(value is IObservationFilter filter)
        {
            var json = JsonConvert.SerializeObject(filter, _settings);
            var jobject = JObject.Parse(json);
            jobject.AddFirst(new JProperty("Type", GetType(filter)));
            jobject.WriteTo(writer);
        }
    }

    public static string GetType(IObservationFilter filter)
    {
        return filter switch
        {
            IsEqualValueFilter _ => "IsEqual",
            IsInTimeBlockFilter _ => "IsInTimeBlock",
            _ => throw new ArgumentException($"Unknown filter type: {filter.GetType().Name}")
        };
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jobject = JObject.Load(reader);
        var type = jobject["Type"]?.Value<string>();
        return type switch
        {
            "IsEqual" => JsonConvert.DeserializeObject<IsEqualValueFilter>(jobject.ToString()),
            "IsInTimeBlock" => JsonConvert.DeserializeObject<IsInTimeBlockFilter>(jobject.ToString()),
            _ => throw new ArgumentException($"Unknown filter type: {type}")
        };
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(IObservationFilter).IsAssignableFrom(objectType);
    }

    public static string Serialize(IObservationFilter filter)
    {
        return filter switch
        {
            IsEqualValueFilter isEqualValueFilter => SerializeFilterWithType(isEqualValueFilter, "IsEqual"),
            IsInTimeBlockFilter isInTimeBlockFilter => SerializeFilterWithType(isInTimeBlockFilter, "IsInTimeBlock"),
            _ => throw new ArgumentException($"Unknown filter type: {filter.GetType().Name}")
        };
    }

    public static IObservationFilter Deserialize(string json)
    {
        var jobject = JObject.Parse(json);
        var type = jobject["Type"]?.Value<string>();
        return type switch
        {
            "IsEqual" => JsonConvert.DeserializeObject<IsEqualValueFilter>(json, _settings) ??
                         throw new ObservationFilterSerializationException("Błąd deserializacji warunku obserwacji typu IsEqual."),
            "IsInTimeBlock" => JsonConvert.DeserializeObject<IsInTimeBlockFilter>(json, _settings) ??
                               throw new ObservationFilterSerializationException("Błąd deserializacji warunku obserwacji typu IsInTimeBlock."),
            _ => throw new ArgumentException($"Unknown filter type: {type}")
        };
    }
}