using Hirundo.App;
using Hirundo.Processors.Computed;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Hirundo.Serialization.Json.Tests;

[TestFixture]
public class ComputedValuesJsonConverterTests
{
    [SetUp]
    public void Initialize()
    {
        _settings = new JsonSerializerSettings
        {
            Converters = [new HirundoJsonConverter()]
        };
    }

    private JsonSerializerSettings _settings = null!;

    [Test]
    public void GivenComputedValue_WhenSerializeAndDeserialize_ShouldReturnComputedValue()
    {
        // Arrange
        var computedValue = new PointednessCalculator("POINTEDNESS", ["D2", "D3", "D4", "D5", "D6", "D7", "D8"], "WING");

        var config = new ApplicationConfig
        {
            ComputedValues = new()
            {
                ComputedValues = [computedValue]
            }
        };

        // Act
        var json = JsonConvert.SerializeObject(config, _settings);
        var result = JsonConvert.DeserializeObject<ApplicationConfig>(json, _settings)!;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ComputedValues, Is.Not.Null);
        Assert.That(result.ComputedValues.ComputedValues, Is.Not.Null);
        Assert.That(result.ComputedValues.ComputedValues.Count, Is.EqualTo(1));
        Assert.That(result.ComputedValues.ComputedValues[0], Is.TypeOf<PointednessCalculator>());

        var pointedness = (PointednessCalculator)result.ComputedValues.ComputedValues[0];

        Assert.That(pointedness.ResultName, Is.EqualTo("POINTEDNESS"));
        var parameters = new[] { "D2", "D3", "D4", "D5", "D6", "D7", "D8" };
        Assert.That(pointedness.WingParameters, Is.EquivalentTo(parameters));
        Assert.That(pointedness.WingName, Is.EqualTo("WING"));
    }
}
