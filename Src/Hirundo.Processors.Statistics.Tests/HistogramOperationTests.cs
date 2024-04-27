using Hirundo.Commons.Models;
using Hirundo.Processors.Statistics.Operations;
using NUnit.Framework;
using System.Collections;

namespace Hirundo.Processors.Statistics.Tests;

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

        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [6])]);

        // Act
        var result = operation.GetStatistics(specimen, populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM_1",
            "HISTOGRAM_2"
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

        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [6])]);

        var operation = new HistogramOperation("VALUE", "HISTOGRAM", 1, 9);

        // Act
        var result = operation.GetStatistics(specimen, populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM_1",
            "HISTOGRAM_2",
            "HISTOGRAM_3",
            "HISTOGRAM_4",
            "HISTOGRAM_5",
            "HISTOGRAM_6",
            "HISTOGRAM_7",
            "HISTOGRAM_8",
            "HISTOGRAM_9"
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

        var specimen = new Specimen("XXX123", [new Observation(["FAT"], [6])]);

        var operation = new HistogramOperation("FAT", "FAT_H", 1, 2);

        // Act
        var result = operation.GetStatistics(specimen, populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList { "FAT_H_1", "FAT_H_2" }));
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

        var specimen = new Specimen("XXX123", [new Observation(["FAT"], [6])]);

        var operation = new HistogramOperation("FAT", "FAT_H", 1, 2);

        // Act
        var result = operation.GetStatistics(specimen, populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList { "FAT_H_1", "FAT_H_2" }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 1, 0 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList { "A1" }));
        Assert.That(result.EmptyValueIds, Is.Empty);
    }

    [Test]
    public void GivenEmptyPopulationData_WhenGetStatistics_ReturnsEmptyHistogram()
    {
        // Arrange
        var operation = new HistogramOperation("VALUE", "HISTOGRAM", 1, 2);
        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [6])]);

        // Act
        var result = operation.GetStatistics(specimen, []);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM_1",
            "HISTOGRAM_2"
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

        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [6])]);

        // Act
        var result = operation.GetStatistics(specimen, populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM_0.0",
            "HISTOGRAM_0.5",
            "HISTOGRAM_1.0",
            "HISTOGRAM_1.5",
            "HISTOGRAM_2.0"
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

        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [6])]);

        // Act
        var result = operation.GetStatistics(specimen, populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM_0.0",
            "HISTOGRAM_0.5",
            "HISTOGRAM_1.0",
            "HISTOGRAM_1.5",
            "HISTOGRAM_2.0"
        }));

        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 0, 0, 0, 0, 0 }));

        Assert.That(result.PopulationIds, Is.Empty);
        Assert.That(result.EmptyValueIds, Is.Empty);
        Assert.That(result.OutlierIds, Is.EquivalentTo(new ArrayList { "A001", "A002" }));
    }

    [Test]
    public void GivenIncludePopulation_WhenGetStatistics_IncludesPopulation()
    {
        // Arrange
        bool includePopulation = true;

        var operation = new HistogramOperation("VALUE", "HISTOGRAM", 0.0m, 2.0m, 0.5m, includePopulation);


        List<Specimen> populationData =
        [
            new Specimen("A001", [new Observation(["VALUE"], [0.1])]),
            new Specimen("A002", [new Observation(["VALUE"], [0.3])]),
            new Specimen("A003", [new Observation(["VALUE"], [0.5])]),
            new Specimen("A004", [new Observation(["VALUE"], [0.7])]),
            new Specimen("A005", [new Observation(["VALUE"], [1.1])]),
            new Specimen("A006", [new Observation(["VALUE"], [1.6])]),
            new Specimen("A007", [new Observation(["VALUE"], [2.5])]),
            new Specimen("A008", [new Observation(["VALUE"], [null])]),
        ];

        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [6])]);

        // Act
        var result = operation.GetStatistics(specimen, populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM_0.0",
            "HISTOGRAM_0.5",
            "HISTOGRAM_1.0",
            "HISTOGRAM_1.5",
            "HISTOGRAM_2.0",
            "HISTOGRAM_POPULATION"
        }));

        Assert.That(operation.IncludePopulation, Is.True);
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 2, 2, 1, 1, 0, 6 }));
    }

    [Test]
    public void GivenIncludeDistribution_WhenGetStatistics_IncludesDistribution()
    {
        // Arrange
        bool includePopulation = false;
        bool includeDistribution = true;
        var operation = new HistogramOperation("VALUE", "HISTOGRAM", 1, 3, 1, includePopulation, includeDistribution);

        List<Specimen> population =
        [
            new Specimen("A001", [new Observation(["VALUE"], [1])]),
            new Specimen("A002", [new Observation(["VALUE"], [1])]),
            new Specimen("A003", [new Observation(["VALUE"], [1])]),
            new Specimen("A004", [new Observation(["VALUE"], [2])]),
            new Specimen("A005", [new Observation(["VALUE"], [2])]),
            new Specimen("A006", [new Observation(["VALUE"], [2])]),
            new Specimen("A007", [new Observation(["VALUE"], [2])]),
            new Specimen("A008", [new Observation(["VALUE"], [2])]),
            new Specimen("A009", [new Observation(["VALUE"], [3])]),
            new Specimen("A010", [new Observation(["VALUE"], [3])]),
            new Specimen("A011", [new Observation(["VALUE"], [3])]),
        ];

        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [2])]);

        var returningSpecimen = new ReturningSpecimen(specimen, population);

        // Act
        var result = operation.GetStatistics(returningSpecimen);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM_1",
            "HISTOGRAM_2",
            "HISTOGRAM_3",
            "HISTOGRAM_DISTRIBUTION"
        }));

        Assert.That(operation.IncludeDistribution, Is.True);
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 3, 5, 3, 0.5m }));
    }

    [Test]
    public void GivenOnlyOneValueDistribution_WhenGetStatistics_ReturnsHalf()
    {
        // Arrange
        bool includePopulation = false;
        bool includeDistribution = true;
        var operation = new HistogramOperation("VALUE", "HISTOGRAM", 1, 3, 1, includePopulation, includeDistribution);

        List<Specimen> population = [];

        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [2])]);

        var returningSpecimen = new ReturningSpecimen(specimen, population);

        // Act
        var result = operation.GetStatistics(returningSpecimen);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM_1",
            "HISTOGRAM_2",
            "HISTOGRAM_3",
            "HISTOGRAM_DISTRIBUTION"
        }));

        Assert.That(operation.IncludeDistribution, Is.True);
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 0, 0, 0, 0.5m }));
    }

    [Test]
    public void GivenValueExceedingHistogram_WhenGetStatistics_ReturnsOne()
    {
        // Arrange
        bool includePopulation = false;
        bool includeDistribution = true;
        var operation = new HistogramOperation("VALUE", "HISTOGRAM", 1, 3, 1, includePopulation, includeDistribution);

        List<Specimen> population = [
             new Specimen("A001", [new Observation(["VALUE"], [1])]),
             new Specimen("A001", [new Observation(["VALUE"], [2])]),
             new Specimen("A001", [new Observation(["VALUE"], [3])])
        ];

        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [4])]);

        var returningSpecimen = new ReturningSpecimen(specimen, population);

        // Act
        var result = operation.GetStatistics(returningSpecimen);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM_1",
            "HISTOGRAM_2",
            "HISTOGRAM_3",
            "HISTOGRAM_DISTRIBUTION"
        }));

        Assert.That(operation.IncludeDistribution, Is.True);
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 1, 1, 1, 1m }));
    }
    [Test]
    public void GivenValueBelowHistogram_WhenGetStatistics_ReturnsZero()
    {
        // Arrange
        bool includePopulation = false;
        bool includeDistribution = true;
        var operation = new HistogramOperation("VALUE", "HISTOGRAM", 1, 3, 1, includePopulation, includeDistribution);

        List<Specimen> population = [
             new Specimen("A001", [new Observation(["VALUE"], [1])]),
             new Specimen("A001", [new Observation(["VALUE"], [2])]),
             new Specimen("A001", [new Observation(["VALUE"], [3])])
        ];

        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [0])]);

        var returningSpecimen = new ReturningSpecimen(specimen, population);

        // Act
        var result = operation.GetStatistics(returningSpecimen);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "HISTOGRAM_1",
            "HISTOGRAM_2",
            "HISTOGRAM_3",
            "HISTOGRAM_DISTRIBUTION"
        }));

        Assert.That(operation.IncludeDistribution, Is.True);
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 1, 1, 1, 0m }));
    }
}