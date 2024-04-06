using Hirundo.Commons.Models;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.Tests;

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
        Assert.That(observation.Headers.Count, Is.EqualTo(2));
        Assert.That(observation.Headers, Contains.Item("NEW_DATA"));
        Assert.That(observation.Values.Count, Is.EqualTo(2));
        Assert.That(observation.Values, Contains.Item("123"));
    }
}