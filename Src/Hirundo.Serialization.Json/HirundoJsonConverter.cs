using Hirundo.Databases;
using Hirundo.Processors.Computed;
using Hirundo.Processors.Observations;
using Hirundo.Processors.Population.Conditions;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.Statistics.Operations;
using Hirundo.Writers;
using Newtonsoft.Json;

namespace Hirundo.Serialization.Json;

public class HirundoJsonConverter : JsonConverter
{
    private static readonly IList<JsonConverter> _converters =
    [
        new DynamicPolymorphicJsonConverter(typeof(IDatabaseParameters)),
        new DynamicPolymorphicJsonConverter(typeof(IComputedValuesCalculator)),
        new DynamicPolymorphicJsonConverter(typeof(IObservationCondition)),
        new DynamicPolymorphicJsonConverter(typeof(IPopulationCondition)),
        new DynamicPolymorphicJsonConverter(typeof(IStatisticalOperation)),
        new DynamicPolymorphicJsonConverter(typeof(IReturningSpecimenCondition)),
        new DynamicPolymorphicJsonConverter(typeof(IWriterParameters))
    ];

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
        ArgumentNullException.ThrowIfNull(writer);

        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        var converter = _converters.First(c => c.CanConvert(value.GetType()));
        converter.WriteJson(writer, value, serializer);
    }
}