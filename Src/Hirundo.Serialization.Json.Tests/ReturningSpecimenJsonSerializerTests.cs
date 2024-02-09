using Hirundo.Processors.Returning;
using Hirundo.Processors.Returning.Conditions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Hirundo.Serialization.Json.Tests;

[TestFixture]
public class ReturningSpecimenJsonSerializerTests
{
    [SetUp]
    public void Setup()
    {
        _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            Converters = new List<JsonConverter>
            {
                new HirundoJsonConverter()
            }
        };
    }

    private JsonSerializerSettings _settings = null!;

    [Test]
    public void GivenReturningSpecimenConditions_WhenSerialize_ReturnsJsonWithConditions()
    {
        // Arrange
        var parameters = new ReturningSpecimensParameters(
            [
                new ReturnsAfterTimePeriodCondition("DATE1", 20),
                new ReturnsNotEarlierThanGivenDateNextYearCondition("DATE2", 07, 15)
            ]
        );

        // Act
        var result = JsonConvert.SerializeObject(parameters, _settings);

        // Assert
        var jobject = JObject.Parse(result);
        Assert.That(jobject["Conditions"]?.Count(), Is.EqualTo(2));
        Assert.That(jobject["Conditions"]?[0]?["Type"]?.Value<string>(), Is.EqualTo("ReturnsAfterTimePeriod"));
        Assert.That(jobject["Conditions"]?[0]?["DateValueName"]?.Value<string>(), Is.EqualTo("DATE1"));
        Assert.That(jobject["Conditions"]?[0]?["TimePeriodInDays"]?.Value<int>(), Is.EqualTo(20));

        Assert.That(jobject["Conditions"]?[1]?["Type"]?.Value<string>(), Is.EqualTo("ReturnsNotEarlierThanGivenDateNextYear"));
        Assert.That(jobject["Conditions"]?[1]?["DateValueName"]?.Value<string>(), Is.EqualTo("DATE2"));
        Assert.That(jobject["Conditions"]?[1]?["Month"]?.Value<int>(), Is.EqualTo(07));
        Assert.That(jobject["Conditions"]?[1]?["Day"]?.Value<int>(), Is.EqualTo(15));
    }

    [Test]
    public void GivenReturningSpecimenConditions_WhenSerializeAndDeserialize_ReturnsSameConditions()
    {
        // Arrange
        var parameters = new ReturningSpecimensParameters([new ReturnsAfterTimePeriodCondition("DATE1", 20), new ReturnsNotEarlierThanGivenDateNextYearCondition("DATE2", 06, 14)]);

        // Act
        var serialized = JsonConvert.SerializeObject(parameters, _settings);
        var deserialized = JsonConvert.DeserializeObject<ReturningSpecimensParameters>(serialized, _settings)!;

        // Assert
        Assert.That(deserialized.Conditions.Count, Is.EqualTo(2));

        Assert.That(deserialized.Conditions[0], Is.TypeOf<ReturnsAfterTimePeriodCondition>());
        Assert.That(deserialized.Conditions[1], Is.TypeOf<ReturnsNotEarlierThanGivenDateNextYearCondition>());

        Assert.That(((ReturnsAfterTimePeriodCondition)deserialized.Conditions[0]).DateValueName, Is.EqualTo("DATE1"));
        Assert.That(((ReturnsAfterTimePeriodCondition)deserialized.Conditions[0]).TimePeriodInDays, Is.EqualTo(20));

        Assert.That(((ReturnsNotEarlierThanGivenDateNextYearCondition)deserialized.Conditions[1]).DateValueName, Is.EqualTo("DATE2"));
        Assert.That(((ReturnsNotEarlierThanGivenDateNextYearCondition)deserialized.Conditions[1]).Month, Is.EqualTo(06));
        Assert.That(((ReturnsNotEarlierThanGivenDateNextYearCondition)deserialized.Conditions[1]).Day, Is.EqualTo(14));
    }
}