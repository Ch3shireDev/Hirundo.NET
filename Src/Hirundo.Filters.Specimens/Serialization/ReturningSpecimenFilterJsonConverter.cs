using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hirundo.Filters.Specimens.Serialization;

public class ReturningSpecimenFilterJsonConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is IReturningSpecimenFilter filter)
        {
            var json = JsonConvert.SerializeObject(filter);
            var jobject = JObject.Parse(json);
            jobject.AddFirst(new JProperty("Type", GetType(filter)));
            jobject.WriteTo(writer);
        }
    }

    private string GetType(IReturningSpecimenFilter filter)
    {
        return filter switch
        {
            ReturnsAfterTimePeriodFilter _ => "ReturnsAfterTimePeriod",
            ReturnsNotEarlierThanGivenDateNextYearFilter _ => "ReturnsNotEarlierThanGivenDateNextYear",
            _ => throw new ArgumentException($"Unknown filter type: {filter.GetType().Name}")
        };
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jobject = JObject.Load(reader);
        var type = jobject["Type"]?.Value<string>();
        return type switch
        {
            "ReturnsAfterTimePeriod" => JsonConvert.DeserializeObject<ReturnsAfterTimePeriodFilter>(jobject.ToString()),
            "ReturnsNotEarlierThanGivenDateNextYear" => JsonConvert.DeserializeObject<ReturnsNotEarlierThanGivenDateNextYearFilter>(jobject.ToString()),
            _ => throw new ArgumentException($"Unknown filter type: {type}")
        };
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(IReturningSpecimenFilter).IsAssignableFrom(objectType);
    }
}