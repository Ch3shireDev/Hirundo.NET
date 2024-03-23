using Hirundo.Processors.Returning;
using Hirundo.Processors.Returning.Conditions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Hirundo.Serialization.Json.Tests;

[TestFixture]
public class ReturnsJsonConverterTests
{
    [SetUp]
    public void Initialize()
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
    public void GivenSimpleCondition_WhenSerialize_ReturnsJsonWithValuesAndType()
    {
        // Arrange
        var condition = new ReturnsAfterTimePeriodCondition("DATE1", 20);
        var parameters = new ReturningSpecimensParameters([condition]);

        // Act
        var result = JsonConvert.SerializeObject(parameters, _settings);

        // Assert
        var jobjectParent = JObject.Parse(result);
        var jobject = jobjectParent["Conditions"]?[0] as JObject ?? throw new InvalidOperationException();
        Assert.That(jobject["Type"]?.Value<string>(), Is.EqualTo("ReturnsAfterTimePeriod"));
        Assert.That(jobject["DateValueName"]?.Value<string>(), Is.EqualTo("DATE1"));
        Assert.That(jobject["TimePeriodInDays"]?.Value<int>(), Is.EqualTo(20));
    }

    [Test]
    public void GivenSerializedCondition_WhenDeserialize_ReturnsSameCondition()
    {
        // Arrange
        var condition = new ReturnsAfterTimePeriodCondition("DATE2", 30);
        var parameters = new ReturningSpecimensParameters([condition]);

        // Act
        var serialized = JsonConvert.SerializeObject(parameters, _settings);
        var deserialized = JsonConvert.DeserializeObject<ReturningSpecimensParameters>(serialized, _settings);

        // Assert
        var result2 = deserialized.Conditions[0];
        Assert.That(result2, Is.TypeOf<ReturnsAfterTimePeriodCondition>());
        var result = (ReturnsAfterTimePeriodCondition)result2!;
        Assert.That(result.DateValueName, Is.EqualTo("DATE2"));
        Assert.That(result.TimePeriodInDays, Is.EqualTo(30));
    }

    [Test]
    public void GivenOtherCondition_WhenSerialize_ReturnsJsonWithValuesAndType()
    {
        // Arrange
        var condition = new ReturnsNotEarlierThanGivenDateNextYearCondition("DATE3", 07, 15);

        // Act
        var result = JsonConvert.SerializeObject(condition, _settings);

        // Assert
        var jobject = JObject.Parse(result);
        Assert.That(jobject["Type"]?.Value<string>(), Is.EqualTo("ReturnsNotEarlierThanGivenDateNextYear"));
        Assert.That(jobject["DateValueName"]?.Value<string>(), Is.EqualTo("DATE3"));
        Assert.That(jobject["Month"]?.Value<int>(), Is.EqualTo(07));
        Assert.That(jobject["Day"]?.Value<int>(), Is.EqualTo(15));
    }

    [Test]
    public void GivenAlternativeCondition_WhenSerialize_ReturnsJsonWithValuesAndType()
    {
        // Arrange
        var condition = new AlternativeReturningCondition(
                        new ReturnsAfterTimePeriodCondition("DATE1", 20),
                        new ReturnsNotEarlierThanGivenDateNextYearCondition("DATE2", 07, 15)
                    );
        var parameters = new ReturningSpecimensParameters([condition]);

        // Act
        var result = JsonConvert.SerializeObject(parameters, _settings);

        // Assert
        var jobject = JObject.Parse(result);
        Assert.That(jobject["Conditions"]?.Count(), Is.EqualTo(1));
        var alternative = jobject["Conditions"]?[0] as JObject;
        Assert.That(alternative?["Type"]?.Value<string>(), Is.EqualTo("Alternative"));
        Assert.That(alternative?["Conditions"]?.Count(), Is.EqualTo(2));
        var firstCondition = alternative?["Conditions"]?[0] as JObject;
        Assert.That(firstCondition?["Type"]?.Value<string>(), Is.EqualTo("ReturnsAfterTimePeriod"));
    }

    [Test]
    public void CanDeserializeAlternativeCondition()
    {
        // Arrange
        var condition = new AlternativeReturningCondition(
                        new ReturnsAfterTimePeriodCondition("DATE1", 20),
                        new ReturnsNotEarlierThanGivenDateNextYearCondition("DATE2", 07, 15)
                    );
        var parameters = new ReturningSpecimensParameters([condition]);

        // Act
        var result = JsonConvert.SerializeObject(parameters, _settings);
        var deserialized = JsonConvert.DeserializeObject<ReturningSpecimensParameters>(result, _settings)!;

        // Assert
        Assert.That(deserialized.Conditions.Count, Is.EqualTo(1));
        Assert.That(deserialized.Conditions[0], Is.TypeOf<AlternativeReturningCondition>());
        var alternative = (AlternativeReturningCondition)deserialized.Conditions[0];
        Assert.That(alternative.Conditions.Count, Is.EqualTo(2));
        Assert.That(alternative.Conditions[0], Is.TypeOf<ReturnsAfterTimePeriodCondition>());
        Assert.That(alternative.Conditions[1], Is.TypeOf<ReturnsNotEarlierThanGivenDateNextYearCondition>());
        var firstCondition = (ReturnsAfterTimePeriodCondition)alternative.Conditions[0];
        Assert.That(firstCondition.DateValueName, Is.EqualTo("DATE1"));
        Assert.That(firstCondition.TimePeriodInDays, Is.EqualTo(20));
        var secondCondition = (ReturnsNotEarlierThanGivenDateNextYearCondition)alternative.Conditions[1];
        Assert.That(secondCondition.DateValueName, Is.EqualTo("DATE2"));
        Assert.That(secondCondition.Month, Is.EqualTo(07));
        Assert.That(secondCondition.Day, Is.EqualTo(15));
    }
}
