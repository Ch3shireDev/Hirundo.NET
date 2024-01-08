using Hirundo.Commons;
using Hirundo.Filters.Observations.Serialization;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Hirundo.Filters.Observations.Tests.Serialization;

[TestFixture]
public class ObservationFilterJsonSerializationTests
{
    [SetUp]
    public void Setup()
    {
        _serializer = new ObservationFilterJsonSerializer();
    }

    private ObservationFilterJsonSerializer _serializer = null!;

    [Test]
    public void GivenIsEqualFilter_WhenSerialize_ReturnsJsonString()
    {
        // Arrange
        var filter = new IsEqualValueFilter("ID", 1);

        // Act
        var json = _serializer.Serialize(filter);

        // Assert
        var jobject = JObject.Parse(json);
        Assert.That(json, Is.Not.Null);
        Assert.That(jobject["Type"]?.Value<string>(), Is.EqualTo("IsEqual"));
    }

    [Test]
    public void GivenIsInTimeBlockFilter_WhenSerialize_ReturnsJsonString()
    {
        // Arrange
        var filter = new IsInTimeBlockFilter("Time", new TimeBlock(6, 12));

        // Act
        var json = _serializer.Serialize(filter);

        // Assert
        var jobject = JObject.Parse(json);
        Assert.That(json, Is.Not.Null);
        Assert.That(jobject["Type"]?.Value<string>(), Is.EqualTo("IsInTimeBlock"));
        Assert.That(jobject["ValueName"]?.Value<string>(), Is.EqualTo("Time"));
        Assert.That(jobject["TimeBlock"]?["StartHour"]?.Value<int>(), Is.EqualTo(6));
        Assert.That(jobject["TimeBlock"]?["EndHour"]?.Value<int>(), Is.EqualTo(12));
    }

    [Test]
    public void GivenIsEqualFilterJson_WhenDeserialize_ReturnsIsEqualFilter()
    {
        // Arrange
        var json = @"{
            ""Type"": ""IsEqual"",
            ""ValueName"": ""ID"",
            ""Value"": 1
        }";

        // Act
        var filter = _serializer.Deserialize(json);

        // Assert
        Assert.That(filter, Is.Not.Null);
        Assert.That(filter, Is.TypeOf<IsEqualValueFilter>());
        var isEqualFilter = (IsEqualValueFilter)filter;
        Assert.That(isEqualFilter.ValueName, Is.EqualTo("ID"));
        Assert.That(isEqualFilter.Value, Is.EqualTo(1));
    }

    [Test]
    public void GivenIsInTimeBlockFilterJson_WhenDeserialize_ReturnsIsInTimeBlockFilter()
    {
        // Arrange
        var json = @"{
            ""Type"": ""IsInTimeBlock"",
            ""ValueName"": ""HOUR"",
            ""TimeBlock"": {
              ""StartHour"": 4,
              ""EndHour"": 10
            }
        }";

        // Act
        var filter = _serializer.Deserialize(json) as IsInTimeBlockFilter;

        // Assert
        Assert.That(filter, Is.Not.Null);
        Assert.That(filter, Is.TypeOf<IsInTimeBlockFilter>());
        Assert.That(filter?.ValueName, Is.EqualTo("HOUR"));
        Assert.That(filter?.TimeBlock.StartHour, Is.EqualTo(4));
        Assert.That(filter?.TimeBlock.EndHour, Is.EqualTo(10));
    }
}