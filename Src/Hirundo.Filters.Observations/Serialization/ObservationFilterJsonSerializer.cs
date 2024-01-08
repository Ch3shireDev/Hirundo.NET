using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Hirundo.Filters.Observations.Serialization;

public class ObservationFilterJsonSerializer
{
    private readonly JsonSerializerSettings _settings = new()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        NullValueHandling = NullValueHandling.Ignore,
        Formatting = Formatting.Indented,
        Converters = new List<JsonConverter>
        {
            new StringEnumConverter()
        }
    };

    public string Serialize(IObservationFilter filter)
    {
        return filter switch
        {
            IsEqualValueFilter isEqualValueFilter => SerializeFilterWithType(isEqualValueFilter, "IsEqual"),
            IsInTimeBlockFilter isInTimeBlockFilter => SerializeFilterWithType(isInTimeBlockFilter, "IsInTimeBlock"),
            _ => throw new ArgumentException($"Unknown filter type: {filter.GetType().Name}")
        };
    }

    private string SerializeFilterWithType(IObservationFilter filter, string type)
    {
        var json = JsonConvert.SerializeObject(filter, _settings);
        var jobject = JObject.Parse(json);
        jobject.AddFirst(new JProperty("Type", type));
        return jobject.ToString();
    }

    public IObservationFilter Deserialize(string json)
    {
        var jobject = JObject.Parse(json);
        var type = jobject["Type"]?.Value<string>();
        return type switch
        {
            "IsEqual" => JsonConvert.DeserializeObject<IsEqualValueFilter>(json, _settings) ?? throw new ObservationFilterSerializationException("Błąd deserializacji warunku obserwacji typu IsEqual."),
            "IsInTimeBlock" => JsonConvert.DeserializeObject<IsInTimeBlockFilter>(json, _settings) ??
                               throw new ObservationFilterSerializationException("Błąd deserializacji warunku obserwacji typu IsInTimeBlock."),
            _ => throw new ArgumentException($"Unknown filter type: {type}")
        };
    }
}