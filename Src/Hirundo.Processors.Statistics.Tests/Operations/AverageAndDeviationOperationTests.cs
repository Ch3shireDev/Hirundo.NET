using Hirundo.Commons;
using Hirundo.Processors.Statistics.Operations;
using Hirundo.Processors.Statistics.Operations.Outliers;
using NUnit.Framework;

namespace Hirundo.Processors.Statistics.Tests.Operations;

[TestFixture]
public class AverageAndDeviationOperationTests
{
    [Test]
    public void GivenSimplePopulationData_WhenGetStatistics_ReturnsAverageOverGivenValue()
    {
        // Arrange
        List<Specimen> populationData =
        [
            new Specimen("ABC123", [new Observation(["VALUE"], [1])]),
            new Specimen("DEF456", [new Observation(["VALUE"], [5])]),
            new Specimen("GHI789", [new Observation(["VALUE"], [9])])
        ];

        var operation = new AverageAndDeviationOperation("VALUE", "AVERAGE_VALUE", "VALUE_SD");

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new List<string> { "AVERAGE_VALUE", "VALUE_SD" }));
        Assert.That(result.Values, Is.EqualTo(new object[] { 5, 4 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new List<string> { "ABC123", "DEF456", "GHI789" }));
    }

    [Test]
    public void GivenNullsInValues_WhenGetStatistics_ReturnsAverageOverGivenValue()
    {
        // Arrange
        List<Specimen> populationData =
        [
            new Specimen("ABC123", [new Observation(["VALUE"], [1])]),
            new Specimen("DEF456", [new Observation(["VALUE"], [3])]),
            new Specimen("GHI789", [new Observation(["VALUE"], [5])]),
            new Specimen("JKL012", [new Observation(["VALUE"], [null])])
        ];

        var outlierDetection = new StandardDeviationOutliersCondition
        {
            RejectOutliers = false
        };
        var operation = new AverageAndDeviationOperation("VALUE", "VALUE_AVG", "VALUE_SD", outlierDetection);

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EqualTo(new List<string> { "VALUE_AVG", "VALUE_SD" }));
        Assert.That(result.Values, Is.EqualTo(new object[] { 3, 2 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new List<string> { "ABC123", "DEF456", "GHI789" }));
        Assert.That(result.EmptyValuesIds, Is.EquivalentTo(new List<string> { "JKL012" }));
    }
}