using Hirundo.Commons;
using Hirundo.Commons.Models;
using Hirundo.Processors.Statistics.Operations;
using NUnit.Framework;
using System.Collections;

namespace Hirundo.Processors.Statistics.Tests.Operations;

[TestFixture]
public class HistogramOperationTests
{
    [Test]
    public void GivenOneValue_WhenGetStatistics_ReturnsHistogram()
    {
        // Arrange
        List<Specimen> populationData =
        [
            new Specimen("ABC123", [new Observation(["VALUE"], [1])])
        ];

        var operation = new HistogramOperation("VALUE", "HISTOGRAM", 1, 2);

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM-1",
            "HISTOGRAM-2"
        }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 1, 0 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList { "ABC123" }));
    }

    [Test]
    public void GivenSimplePopulationData_WhenGetStatistics_ReturnsHistogram()
    {
        // Arrange
        List<Specimen> populationData =
        [
            new Specimen("ABC123", [new Observation(["VALUE"], [1])]),
            new Specimen("DEF456", [new Observation(["VALUE"], [5])]),
            new Specimen("GHI789", [new Observation(["VALUE"], [9])])
        ];

        var operation = new HistogramOperation("VALUE", "HISTOGRAM", 1, 9);

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM-1",
            "HISTOGRAM-2",
            "HISTOGRAM-3",
            "HISTOGRAM-4",
            "HISTOGRAM-5",
            "HISTOGRAM-6",
            "HISTOGRAM-7",
            "HISTOGRAM-8",
            "HISTOGRAM-9"
        }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 1, 0, 0, 0, 1, 0, 0, 0, 1 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList { "ABC123", "DEF456", "GHI789" }));
    }

    [Test]
    public void GivenNullsInValues_WhenGetStatistics_ReturnsHistogram()
    {
        // Arrange
        List<Specimen> populationData =
        [
            new Specimen("A1", [new Observation(["FAT"], [1])]),
            new Specimen("A2", [new Observation(["FAT"], [1])]),
            new Specimen("A3", [new Observation(["FAT"], [2])]),
            new Specimen("B1", [new Observation(["FAT"], [null])])
        ];

        var operation = new HistogramOperation("FAT", "FAT-H", 1, 2);

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList { "FAT-H-1", "FAT-H-2" }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 2, 1 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList { "A1", "A2", "A3" }));
        Assert.That(result.EmptyValueIds, Is.EquivalentTo(new ArrayList { "B1" }));
    }

    [Test]
    public void GivenMultipleObservations_WhenGetStatistics_TakesOnlyFirst()
    {
        // Arrange
        List<Specimen> populationData =
        [
            new Specimen("A1", [
                new Observation(["FAT"], [1]),
                new Observation(["FAT"], [2])
            ])
        ];

        var operation = new HistogramOperation("FAT", "FAT-H", 1, 2);

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList { "FAT-H-1", "FAT-H-2" }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 1, 0 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList { "A1" }));
        Assert.That(result.EmptyValueIds, Is.Empty);
    }

    [Test]
    public void GivenEmptyPopulationData_WhenGetStatistics_ReturnsEmptyHistogram()
    {
        // Arrange
        var operation = new HistogramOperation("VALUE", "HISTOGRAM", 1, 2);

        // Act
        var result = operation.GetStatistics([]);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM-1",
            "HISTOGRAM-2"
        }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 0, 0 }));
        Assert.That(result.PopulationIds, Is.Empty);
    }

    [Test]
    public void GivenHalfInterval_WhenGetStatistics_ReturnsValuesWithHalfIntervals()
    {
        // Arrange
        var operation = new HistogramOperation("VALUE", "HISTOGRAM", 0.0m, 2.0m, 0.5m);

        List<Specimen> populationData =
        [
            new Specimen("A001", [new Observation(["VALUE"], [0.1])]),
            new Specimen("A002", [new Observation(["VALUE"], [0.3])]),
            new Specimen("A003", [new Observation(["VALUE"], [0.5])]),
            new Specimen("A004", [new Observation(["VALUE"], [0.7])]),
            new Specimen("A005", [new Observation(["VALUE"], [1.1])]),
            new Specimen("A006", [new Observation(["VALUE"], [1.6])])
        ];

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM-0.0",
            "HISTOGRAM-0.5",
            "HISTOGRAM-1.0",
            "HISTOGRAM-1.5",
            "HISTOGRAM-2.0"
        }));

        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 2, 2, 1, 1, 0 }));
    }

    [Test]
    public void GivenValuesOutsideOfRange_WhenGetStatistics_AddsToOutliers()
    {
        // Arrange
        var operation = new HistogramOperation("VALUE", "HISTOGRAM", 0.0m, 2.0m, 0.5m);

        List<Specimen> populationData =
        [
            new Specimen("A001", [new Observation(["VALUE"], [-0.1])]),
            new Specimen("A002", [new Observation(["VALUE"], [2.7])])
        ];

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM-0.0",
            "HISTOGRAM-0.5",
            "HISTOGRAM-1.0",
            "HISTOGRAM-1.5",
            "HISTOGRAM-2.0"
        }));

        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 0, 0, 0, 0, 0 }));

        Assert.That(result.PopulationIds, Is.Empty);
        Assert.That(result.EmptyValueIds, Is.Empty);
        Assert.That(result.OutlierIds, Is.EquivalentTo(new ArrayList { "A001", "A002" }));
    }
}