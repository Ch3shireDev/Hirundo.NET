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
            Converters =
            [
                new HirundoJsonConverter()
            ]
        };
    }

    private JsonSerializerSettings _settings = null!;

    [Test]
    public void GivenReturningSpecimenConditions_WhenSerialize_ReturnsJsonWithConditions()
    {
        // Arrange
        var parameters = new ReturningParameters(
            [
                new ReturnsAfterTimePeriodCondition(20),
                new ReturnsNotEarlierThanGivenDateNextYearCondition(07, 15)
            ]
        );

        // Act
        var result = JsonConvert.SerializeObject(parameters, _settings);

        // Assert
        var jobject = JObject.Parse(result);
        Assert.That(jobject["Conditions"]?.Count(), Is.EqualTo(2));
        Assert.That(jobject["Conditions"]?[0]?["Type"]?.Value<string>(), Is.EqualTo("ReturnsAfterTimePeriod"));
        Assert.That(jobject["Conditions"]?[0]?["TimePeriodInDays"]?.Value<int>(), Is.EqualTo(20));

        Assert.That(jobject["Conditions"]?[1]?["Type"]?.Value<string>(), Is.EqualTo("ReturnsNotEarlierThanGivenDateNextYear"));
        Assert.That(jobject["Conditions"]?[1]?["Month"]?.Value<int>(), Is.EqualTo(07));
        Assert.That(jobject["Conditions"]?[1]?["Day"]?.Value<int>(), Is.EqualTo(15));
    }

    [Test]
    public void GivenReturningSpecimenConditions_WhenSerializeAndDeserialize_ReturnsSameConditions()
    {
        // Arrange
        var parameters = new ReturningParameters([new ReturnsAfterTimePeriodCondition(20), new ReturnsNotEarlierThanGivenDateNextYearCondition(06, 14)]);

        // Act
        var serialized = JsonConvert.SerializeObject(parameters, _settings);
        var deserialized = JsonConvert.DeserializeObject<ReturningParameters>(serialized, _settings)!;

        // Assert
        Assert.That(deserialized.Conditions.Count, Is.EqualTo(2));

        Assert.That(deserialized.Conditions[0], Is.TypeOf<ReturnsAfterTimePeriodCondition>());
        Assert.That(deserialized.Conditions[1], Is.TypeOf<ReturnsNotEarlierThanGivenDateNextYearCondition>());

        Assert.That(((ReturnsAfterTimePeriodCondition)deserialized.Conditions[0]).TimePeriodInDays, Is.EqualTo(20));

        Assert.That(((ReturnsNotEarlierThanGivenDateNextYearCondition)deserialized.Conditions[1]).Month, Is.EqualTo(06));
        Assert.That(((ReturnsNotEarlierThanGivenDateNextYearCondition)deserialized.Conditions[1]).Day, Is.EqualTo(14));
    }

    [Test]
    public void GivenAlternativeReturningSpecimenConditions_WhenSerialize_ReturnsJsonWithConditions()
    {
        // Arrange
        var condition = new AlternativeReturningCondition(
            new ReturnsAfterTimePeriodCondition(20),
            new ReturnsNotEarlierThanGivenDateNextYearCondition(07, 15)
        );
        var parameters = new ReturningParameters([condition]);

        // Act
        var result = JsonConvert.SerializeObject(parameters, _settings);

        // Assert
        var parametersJson = JObject.Parse(result);

        Assert.That(parametersJson["Conditions"]?.Count(), Is.EqualTo(1));

        var alternativeCondition = parametersJson["Conditions"]?[0] ?? throw new InvalidOperationException();

        Assert.That(alternativeCondition["Type"]?.Value<string>(), Is.EqualTo("Alternative"));
        var conditions = alternativeCondition["Conditions"] as JArray ?? throw new InvalidOperationException();
        Assert.That(conditions.Count, Is.EqualTo(2));

        var firstCondition = conditions[0] as JObject ?? throw new InvalidOperationException();
        Assert.That(firstCondition["Type"]?.Value<string>(), Is.EqualTo("ReturnsAfterTimePeriod"));
        Assert.That(firstCondition["TimePeriodInDays"]?.Value<int>(), Is.EqualTo(20));

        var secondCondition = conditions[1] as JObject ?? throw new InvalidOperationException();
        Assert.That(secondCondition["Type"]?.Value<string>(), Is.EqualTo("ReturnsNotEarlierThanGivenDateNextYear"));
        Assert.That(secondCondition["Month"]?.Value<int>(), Is.EqualTo(07));
        Assert.That(secondCondition["Day"]?.Value<int>(), Is.EqualTo(15));
    }

    [Test]
    public void GivenAlternativeReturningSpecimenConditions_WhenSerializeAndDeserialize_ReturnsSameValue()
    {
        // Arrange
        var condition = new AlternativeReturningCondition(
            new ReturnsAfterTimePeriodCondition(20),
            new ReturnsNotEarlierThanGivenDateNextYearCondition(07, 15)
        );
        var parameters = new ReturningParameters([condition]);

        // Act
        var result = JsonConvert.SerializeObject(parameters, _settings);
        var resultParameters = JsonConvert.DeserializeObject<ReturningParameters>(result, _settings)!;

        // Assert
        Assert.That(resultParameters.Conditions.Count, Is.EqualTo(1));
        Assert.That(resultParameters.Conditions[0], Is.TypeOf<AlternativeReturningCondition>());
        var alternative = (AlternativeReturningCondition)resultParameters.Conditions[0];
        Assert.That(alternative.Conditions.Count, Is.EqualTo(2));

        var firstCondition = alternative.Conditions[0] as ReturnsAfterTimePeriodCondition;
        Assert.That(firstCondition, Is.Not.Null);
        Assert.That(firstCondition!.TimePeriodInDays, Is.EqualTo(20));

        var secondCondition = alternative.Conditions[1] as ReturnsNotEarlierThanGivenDateNextYearCondition;
        Assert.That(secondCondition, Is.Not.Null);
        Assert.That(secondCondition!.Month, Is.EqualTo(07));
        Assert.That(secondCondition.Day, Is.EqualTo(15));
    }

    [Test]
    public void GivenSimpleCondition_WhenSerialize_ReturnsJsonWithValuesAndType()
    {
        // Arrange
        var condition = new ReturnsAfterTimePeriodCondition(20);
        var parameters = new ReturningParameters([condition]);

        // Act
        var result = JsonConvert.SerializeObject(parameters, _settings);

        // Assert
        var jobjectParent = JObject.Parse(result);
        var jobject = jobjectParent["Conditions"]?[0] as JObject ?? throw new InvalidOperationException();
        Assert.That(jobject["Type"]?.Value<string>(), Is.EqualTo("ReturnsAfterTimePeriod"));
        Assert.That(jobject["TimePeriodInDays"]?.Value<int>(), Is.EqualTo(20));
    }

    [Test]
    public void GivenSerializedCondition_WhenDeserialize_ReturnsSameCondition()
    {
        // Arrange
        var condition = new ReturnsAfterTimePeriodCondition(30);
        var parameters = new ReturningParameters([condition]);

        // Act
        var serialized = JsonConvert.SerializeObject(parameters, _settings);
        var deserialized = JsonConvert.DeserializeObject<ReturningParameters>(serialized, _settings)!;

        // Assert
        var result2 = deserialized.Conditions[0];
        Assert.That(result2, Is.TypeOf<ReturnsAfterTimePeriodCondition>());
        var result = (ReturnsAfterTimePeriodCondition)result2!;
        Assert.That(result.TimePeriodInDays, Is.EqualTo(30));
    }

    [Test]
    public void GivenOtherCondition_WhenSerialize_ReturnsJsonWithValuesAndType()
    {
        // Arrange
        var condition = new ReturnsNotEarlierThanGivenDateNextYearCondition(07, 15);

        // Act
        var result = JsonConvert.SerializeObject(condition, _settings);

        // Assert
        var jobject = JObject.Parse(result);
        Assert.That(jobject["Type"]?.Value<string>(), Is.EqualTo("ReturnsNotEarlierThanGivenDateNextYear"));
        Assert.That(jobject["Month"]?.Value<int>(), Is.EqualTo(07));
        Assert.That(jobject["Day"]?.Value<int>(), Is.EqualTo(15));
    }

    [Test]
    public void GivenAlternativeCondition_WhenSerialize_ReturnsJsonWithValuesAndType()
    {
        // Arrange
        var condition = new AlternativeReturningCondition(
            new ReturnsAfterTimePeriodCondition(20),
            new ReturnsNotEarlierThanGivenDateNextYearCondition(07, 15)
        );
        var parameters = new ReturningParameters([condition]);

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
            new ReturnsAfterTimePeriodCondition(20),
            new ReturnsNotEarlierThanGivenDateNextYearCondition(07, 15)
        );
        var parameters = new ReturningParameters([condition]);

        // Act
        var result = JsonConvert.SerializeObject(parameters, _settings);
        var deserialized = JsonConvert.DeserializeObject<ReturningParameters>(result, _settings)!;

        // Assert
        Assert.That(deserialized.Conditions.Count, Is.EqualTo(1));
        Assert.That(deserialized.Conditions[0], Is.TypeOf<AlternativeReturningCondition>());
        var alternative = (AlternativeReturningCondition)deserialized.Conditions[0];
        Assert.That(alternative.Conditions.Count, Is.EqualTo(2));
        Assert.That(alternative.Conditions[0], Is.TypeOf<ReturnsAfterTimePeriodCondition>());
        Assert.That(alternative.Conditions[1], Is.TypeOf<ReturnsNotEarlierThanGivenDateNextYearCondition>());
        var firstCondition = (ReturnsAfterTimePeriodCondition)alternative.Conditions[0];
        Assert.That(firstCondition.TimePeriodInDays, Is.EqualTo(20));
        var secondCondition = (ReturnsNotEarlierThanGivenDateNextYearCondition)alternative.Conditions[1];
        Assert.That(secondCondition.Month, Is.EqualTo(07));
        Assert.That(secondCondition.Day, Is.EqualTo(15));
    }
}