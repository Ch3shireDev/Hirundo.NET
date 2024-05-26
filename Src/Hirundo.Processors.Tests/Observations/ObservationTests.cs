using Hirundo.Commons.Models;

namespace Hirundo.Processors.Tests.Observations;

[TestFixture]
public class ObservationTests
{
    [Test]
    public void GivenInitializedDataFromArray_WhenAddColumn_AddsColumnWithoutError()
    {
        // Arrange
        string[] headers = ["DATA"];
        object?[] values = ["XYZ"];

        var observation = new Observation
        {
            Ring = "AB123",
            Date = new DateTime(2021, 06, 01),
            Headers = headers,
            Values = values
        };

        // Act
        observation.AddColumn("NEW_DATA", "123");

        // Assert
        Assert.That(observation.Headers, Has.Count.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(observation.Headers, Contains.Item("NEW_DATA"));
            Assert.That(observation.Values, Has.Count.EqualTo(2));
        });
        Assert.That(observation.Values, Contains.Item("123"));
    }
}