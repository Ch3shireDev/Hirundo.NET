using System.Collections;
using Hirundo.Commons;
using Hirundo.Processors.Statistics.Operations;
using Hirundo.Processors.Statistics.Operations.Outliers;
using NUnit.Framework;

namespace Hirundo.Processors.Statistics.Tests.Operations;

[TestFixture]
public class AverageOperationTests
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

        var operation = new AverageOperation("VALUE", "AVERAGE_VALUE", "VALUE_SD");

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList { "AVERAGE_VALUE", "VALUE_SD" }));
        Assert.That(result.Values, Is.EqualTo(new ArrayList { 5, 4 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList { "ABC123", "DEF456", "GHI789" }));
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

        var operation = new AverageOperation("VALUE", "VALUE_AVG", "VALUE_SD", outlierDetection);

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList { "VALUE_AVG", "VALUE_SD" }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 3, 2 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList { "ABC123", "DEF456", "GHI789" }));
        Assert.That(result.EmptyValueIds, Is.EquivalentTo(new ArrayList { "JKL012" }));
    }

    [Test]
    public void GivenOutlierInValues_WhenGetStatistics_ExcludesOutlier()
    {
        // Arrange
        List<Specimen> populationData =
        [
            new Specimen("AAA111", [new Observation(["VALUE"], [10])]),
            new Specimen("BBB222", [new Observation(["VALUE"], [14])]),
            new Specimen("CCC333", [new Observation(["VALUE"], [18])]),
            new Specimen("DDD444", [new Observation(["VALUE"], [9999])])
        ];

        var outlierDetection = new StandardDeviationOutliersCondition
        {
            RejectOutliers = true,
            Threshold = 1
        };

        var operation = new AverageOperation("VALUE", "VALUE_AVG", "VALUE_SD", outlierDetection);

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList { "VALUE_AVG", "VALUE_SD" }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 14, 4 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList { "AAA111", "BBB222", "CCC333" }));
        Assert.That(result.OutlierIds, Is.EquivalentTo(new ArrayList { "DDD444" }));
    }

    [Test]
    public void GivenEmptyPopulation_WhenGetStatistics_ReturnsEmptyResult()
    {
        // Arrange
        var operation = new AverageOperation("VALUE", "VALUE_AVG", "VALUE_SD");

        // Act
        var result = operation.GetStatistics(new List<Specimen>());

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList { "VALUE_AVG", "VALUE_SD" }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { null, null }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList()));
    }

    [Test]
    public void GivenEmptyValues_WhenGetStatistics_ReturnsEmptyResult()
    {
        // Arrange
        var operation = new AverageOperation("VALUE", "VALUE_AVG", "VALUE_SD");

        List<Specimen> population =
        [
            new Specimen("AAA111", [new Observation(["VALUE"], [null])]),
            new Specimen("BBB222", [new Observation(["VALUE"], [null])]),
            new Specimen("CCC333", [new Observation(["VALUE"], [null])])
        ];

        // Act
        var result = operation.GetStatistics(population);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList { "VALUE_AVG", "VALUE_SD" }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { null, null }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList()));
        Assert.That(result.EmptyValueIds, Is.EquivalentTo(new ArrayList { "AAA111", "BBB222", "CCC333" }));
        Assert.That(result.OutlierIds, Is.EquivalentTo(new ArrayList()));
    }
}