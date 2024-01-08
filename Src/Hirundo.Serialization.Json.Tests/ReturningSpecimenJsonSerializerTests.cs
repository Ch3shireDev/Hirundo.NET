using Hirundo.Filters.Specimens;
using Hirundo.Filters.Specimens.Serialization;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Hirundo.Serialization.Json.Tests;

[TestFixture]
public class ReturningSpecimenJsonSerializerTests
{
    [SetUp]
    public void Setup()
    {
        _serializer = new ReturningSpecimenJsonSerializer();
    }

    private ReturningSpecimenJsonSerializer _serializer = null!;

    [Test]
    public void GivenReturningSpecimenConditions_WhenSerialize_ReturnsJsonWithConditions()
    {
        // Arrange
        var parameters = new ReturningSpecimensParameters
        {
            Conditions =
            [
                new ReturnsAfterTimePeriodFilter("DATE1", 20),
                new ReturnsNotEarlierThanGivenDateNextYearFilter("DATE2", 07, 15)
            ]
        };

        // Act
        var result = _serializer.Serialize(parameters);

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
        var parameters = new ReturningSpecimensParameters
        {
            Conditions =
            [
                new ReturnsAfterTimePeriodFilter("DATE1", 20),
                new ReturnsNotEarlierThanGivenDateNextYearFilter("DATE2", 06, 14)
            ]
        };

        // Act
        var serialized = _serializer.Serialize(parameters);
        var deserialized = _serializer.Deserialize(serialized);

        // Assert
        Assert.That(deserialized.Conditions.Count, Is.EqualTo(2));

        Assert.That(deserialized.Conditions[0], Is.TypeOf<ReturnsAfterTimePeriodFilter>());
        Assert.That(deserialized.Conditions[1], Is.TypeOf<ReturnsNotEarlierThanGivenDateNextYearFilter>());

        Assert.That(((ReturnsAfterTimePeriodFilter)deserialized.Conditions[0]).DateValueName, Is.EqualTo("DATE1"));
        Assert.That(((ReturnsAfterTimePeriodFilter)deserialized.Conditions[0]).TimePeriodInDays, Is.EqualTo(20));

        Assert.That(((ReturnsNotEarlierThanGivenDateNextYearFilter)deserialized.Conditions[1]).DateValueName, Is.EqualTo("DATE2"));
        Assert.That(((ReturnsNotEarlierThanGivenDateNextYearFilter)deserialized.Conditions[1]).Month, Is.EqualTo(06));
        Assert.That(((ReturnsNotEarlierThanGivenDateNextYearFilter)deserialized.Conditions[1]).Day, Is.EqualTo(14));
    }
}