using Hirundo.Commons.Models;
using Hirundo.Processors.Statistics.Operations;
using Hirundo.Processors.Statistics.Operations.Outliers;
using NUnit.Framework;
using System.Collections;

namespace Hirundo.Processors.Statistics.Tests;

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

        var operation = new AverageOperation("VALUE", "VALUE_PREFIX");

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList {
            "VALUE_PREFIX_AVERAGE",
            "VALUE_PREFIX_STANDARD_DEVIATION",
            "VALUE_PREFIX_POPULATION_SIZE",
            "VALUE_PREFIX_EMPTY_SIZE",
            "VALUE_PREFIX_OUTLIER_SIZE"
        }));
        Assert.That(result.Values, Is.EqualTo(new ArrayList { 5, 4, 3, 0, 0 }));
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

        var operation = new AverageOperation("VALUE", "PREFIX", outlierDetection);

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList {
            "PREFIX_AVERAGE",
            "PREFIX_STANDARD_DEVIATION",
            "PREFIX_POPULATION_SIZE",
            "PREFIX_EMPTY_SIZE",
            "PREFIX_OUTLIER_SIZE"
        }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 3, 2, 3, 1, 0 }));
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
            new Specimen("BBB221", [new Observation(["VALUE"], [14])]),
            new Specimen("CCC333", [new Observation(["VALUE"], [18])]),
            new Specimen("DDD444", [new Observation(["VALUE"], [9999])])
        ];

        var outlierDetection = new StandardDeviationOutliersCondition
        {
            RejectOutliers = true,
            Threshold = 1
        };

        var operation = new AverageOperation("VALUE", "PREFIX", outlierDetection);

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList {
            "PREFIX_AVERAGE",
            "PREFIX_STANDARD_DEVIATION",
            "PREFIX_POPULATION_SIZE",
            "PREFIX_EMPTY_SIZE",
            "PREFIX_OUTLIER_SIZE"
        }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 14, 4, 3, 0, 1 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList { "AAA111", "BBB221", "CCC333" }));
        Assert.That(result.OutlierIds, Is.EquivalentTo(new ArrayList { "DDD444" }));
    }

    [Test]
    public void GivenEmptyPopulation_WhenGetStatistics_ReturnsEmptyResult()
    {
        // Arrange
        var operation = new AverageOperation("VALUE", "PREFIX");

        // Act
        var result = operation.GetStatistics([]);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList {
            "PREFIX_AVERAGE",
            "PREFIX_STANDARD_DEVIATION",
            "PREFIX_POPULATION_SIZE",
            "PREFIX_EMPTY_SIZE",
            "PREFIX_OUTLIER_SIZE",
        }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { null, null, 0, 0, 0 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList()));
    }

    [Test]
    public void GivenEmptyValues_WhenGetStatistics_ReturnsEmptyResult()
    {
        // Arrange
        var operation = new AverageOperation("VALUE", "PREFIX");

        List<Specimen> population =
        [
            new Specimen("AAA111", [new Observation(["VALUE"], [null])]),
            new Specimen("BBB222", [new Observation(["VALUE"], [null])]),
            new Specimen("CCC333", [new Observation(["VALUE"], [null])])
        ];

        // Act
        var result = operation.GetStatistics(population);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList {
            "PREFIX_AVERAGE",
            "PREFIX_STANDARD_DEVIATION",
            "PREFIX_POPULATION_SIZE",
            "PREFIX_EMPTY_SIZE",
            "PREFIX_OUTLIER_SIZE"
        }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { null, null, 0, 3, 0 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList()));
        Assert.That(result.EmptyValueIds, Is.EquivalentTo(new ArrayList { "AAA111", "BBB222", "CCC333" }));
        Assert.That(result.OutlierIds, Is.EquivalentTo(new ArrayList()));
        var populationSizeIndex = result.Names.IndexOf("PREFIX_POPULATION_SIZE");
        var populationSize = result.Values[populationSizeIndex];
        Assert.That(populationSize, Is.EqualTo(0));
        var emptySizeIndex = result.Names.IndexOf("PREFIX_EMPTY_SIZE");
        var emptySize = result.Values[emptySizeIndex];
        Assert.That(emptySize, Is.EqualTo(3));
        var outlierSizeIndex = result.Names.IndexOf("PREFIX_OUTLIER_SIZE");
        var outlierSize = result.Values[outlierSizeIndex];
        Assert.That(outlierSize, Is.EqualTo(0));
    }

    [Test]
    public void GivenSomeEmptyValues_WhenCalculatingPopulation_ReturnsPopulationSizeOnlyOfNonEmptyValues()
    {
        // Arrange
        var operation = new AverageOperation("VALUE", "PREFIX");

        List<Specimen> population =
        [
            new Specimen("AAA111", [new Observation(["VALUE"], [1])]),
            new Specimen("BBB222", [new Observation(["VALUE"], [2])]),
            new Specimen("CCC333", [new Observation(["VALUE"], [null])])
        ];

        // Act
        var result = operation.GetStatistics(population);

        // Assert
        var populationSizeIndex = result.Names.IndexOf("PREFIX_POPULATION_SIZE");
        var populationSize = result.Values[populationSizeIndex];
        Assert.That(populationSize, Is.EqualTo(2));
    }
}