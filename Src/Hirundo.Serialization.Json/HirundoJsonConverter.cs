using Hirundo.Databases;
using Hirundo.Filters.Observations;
using Hirundo.Filters.Specimens;
using Hirundo.Processors.Population.Conditions;
using Hirundo.Processors.Statistics.Operations;
using Hirundo.Writers.Summary;
using Newtonsoft.Json;

namespace Hirundo.Serialization.Json;

public class HirundoJsonConverter : JsonConverter
{
    private static readonly IList<JsonConverter> _converters = new List<JsonConverter>
    {
        new PolymorphicJsonConverter<IDatabaseParameters>(),
        new PolymorphicJsonConverter<IObservationFilter>(),
        new PolymorphicJsonConverter<IPopulationFilterBuilder>(),
        new PolymorphicJsonConverter<IStatisticalOperation>(),
        new PolymorphicJsonConverter<IReturningSpecimenFilter>(),
        new PolymorphicJsonConverter<IWriterParameters>()
    };

    public override bool CanConvert(Type objectType)
    {
        return _converters.Any(c => c.CanConvert(objectType));
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var converter = _converters.First(c => c.CanConvert(objectType));
        return converter.ReadJson(reader, objectType, existingValue, serializer);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        var converter = _converters.First(c => c.CanConvert(value.GetType()));
        converter.WriteJson(writer, value, serializer);
    }
}