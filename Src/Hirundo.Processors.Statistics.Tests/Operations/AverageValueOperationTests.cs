using Hirundo.Commons;
using Hirundo.Processors.Statistics.Operations;
using NUnit.Framework;

namespace Hirundo.Processors.Statistics.Tests.Operations;

[TestFixture]
public class AverageValueOperationTests
{
    [Test]
    public void GivenSimplePopulationData_WhenGetStatistics_ReturnsAverageOverGivenValue()
    {
        // Arrange
        List<Specimen> populationData =
        [
            new Specimen("ABC123", [new Observation(["VALUE"], [2])]),
            new Specimen("DEF456", [new Observation(["VALUE"], [1])]),
            new Specimen("GHI789", [new Observation(["VALUE"], [6])])
        ];

        var operation = new AverageValueOperation("VALUE", "AVERAGE_VALUE");

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new List<string> { "AVERAGE_VALUE" }));
        Assert.That(result.Values, Is.EqualTo(new object[] { 3 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new List<string> { "ABC123", "DEF456", "GHI789" }));
    }

    [Test]
    public void GivenNullsInValues_WhenGetStatistics_ReturnsAverageOverGivenValue()
    {
        // Arrange
        List<Specimen> populationData =
        [
            new Specimen("ABC123", [new Observation(["VALUE"], [2])]),
            new Specimen("DEF456", [new Observation(["VALUE"], [1])]),
            new Specimen("GHI789", [new Observation(["VALUE"], [6])]),
            new Specimen("JKL012", [new Observation(["VALUE"], [null])])
        ];

        var operation = new AverageValueOperation("VALUE", "AVERAGE_VALUE");

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EqualTo(new List<string> { "AVERAGE_VALUE" }));
        Assert.That(result.Values, Is.EqualTo(new object[] { 3 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new List<string> { "ABC123", "DEF456", "GHI789" }));
        Assert.That(result.EmptyValuesIds, Is.EquivalentTo(new List<string> { "JKL012" }));
    }
}