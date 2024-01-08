using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Hirundo.Filters.Specimens.Serialization;

public class ReturningSpecimenJsonSerializer
{
    private readonly JsonSerializerSettings _settings = new()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        Formatting = Formatting.Indented,
        Converters = new List<JsonConverter>
        {
            new StringEnumConverter(),
            new ReturningSpecimenFilterJsonConverter()
        }
    };

    public string Serialize(ReturningSpecimensParameters parameters)
    {
        return JsonConvert.SerializeObject(parameters, _settings);
    }

    public ReturningSpecimensParameters Deserialize(string json)
    {
        return JsonConvert.DeserializeObject<ReturningSpecimensParameters>(json, _settings) ??
               throw new ReturningSpecimenFilterSerializationException("Błąd deserializacji warunku obserwacji typu IsEqual.");
    }
}