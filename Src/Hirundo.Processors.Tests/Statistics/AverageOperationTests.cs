using Hirundo.Commons.Models;
using Hirundo.Processors.Statistics.Operations;
using System.Collections;

namespace Hirundo.Processors.Tests.Statistics;

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
            new Specimen("GHI789", [new Observation(["VALUE"], [9])])
        ];

        var operation = new AverageOperation("VALUE", "VALUE_PREFIX");

        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [6])]);

        // Act
        var result = operation.GetStatistics(specimen, populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "VALUE_PREFIX_AVERAGE",
            "VALUE_PREFIX_STANDARD_DEVIATION",
            "VALUE_PREFIX_POPULATION_SIZE",
            "VALUE_PREFIX_EMPTY_SIZE",
            "VALUE_PREFIX_OUTLIER_SIZE"
        }));
        Assert.That(result.Values, Is.EqualTo(new ArrayList { 5, 4, 2, 0, 0 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList { "ABC123", "GHI789" }));
    }

    [Test]
    public void GivenNullsInValues_WhenGetStatistics_ReturnsAverageOverGivenValue()
    {
        // Arrange
        List<Specimen> populationData =
        [
            new Specimen("ABC123", [new Observation(["VALUE"], [1])]),
            new Specimen("GHI789", [new Observation(["VALUE"], [5])]),
            new Specimen("JKL012", [new Observation(["VALUE"], [null])])
        ];

        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [6])]);

        var operation = new AverageOperation("VALUE", "PREFIX");
        operation.Outliers.RejectOutliers = false;

        // Act
        var result = operation.GetStatistics(specimen, populationData);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "PREFIX_AVERAGE",
            "PREFIX_STANDARD_DEVIATION",
            "PREFIX_POPULATION_SIZE",
            "PREFIX_EMPTY_SIZE",
            "PREFIX_OUTLIER_SIZE"
        }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 3, 2, 2, 1, 0 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList { "ABC123", "GHI789" }));
        Assert.That(result.EmptyValueIds, Is.EquivalentTo(new ArrayList { "JKL012" }));
    }

    [Test]
    public void GivenOutlierInValues_WhenGetStatistics_ExcludesOutlier()
    {
        // Arrange
        List<Specimen> population =
        [
            new Specimen("AAA111", [new Observation(["VALUE"], [10])]),
            new Specimen("CCC333", [new Observation(["VALUE"], [18])]),
            new Specimen("DDD444", [new Observation(["VALUE"], [9999])])
        ];


        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [6])]);

        var operation = new AverageOperation("VALUE", "PREFIX");
        operation.Outliers.RejectOutliers = true;
        operation.Outliers.Threshold = 1;

        // Act
        var result = operation.GetStatistics(specimen, population);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "PREFIX_AVERAGE",
            "PREFIX_STANDARD_DEVIATION",
            "PREFIX_POPULATION_SIZE",
            "PREFIX_EMPTY_SIZE",
            "PREFIX_OUTLIER_SIZE"
        }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { 14, 4, 2, 0, 1 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList { "AAA111", "CCC333" }));
        Assert.That(result.OutlierIds, Is.EquivalentTo(new ArrayList { "DDD444" }));
    }

    [Test]
    public void GivenEmptyPopulation_WhenGetStatistics_ReturnsEmptyResult()
    {
        // Arrange
        var operation = new AverageOperation("VALUE", "PREFIX");
        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [6])]);

        // Act
        var result = operation.GetStatistics(specimen, []);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "PREFIX_AVERAGE",
            "PREFIX_STANDARD_DEVIATION",
            "PREFIX_POPULATION_SIZE",
            "PREFIX_EMPTY_SIZE",
            "PREFIX_OUTLIER_SIZE"
        }));
        Assert.That(result.Values, Is.EquivalentTo(new ArrayList { null, null, 0, 0, 0 }));
        Assert.That(result.PopulationIds, Is.EquivalentTo(new ArrayList()));
    }

    [Test]
    public void GivenEmptyValues_WhenGetStatistics_ReturnsEmptyResult()
    {
        // Arrange
        var operation = new AverageOperation("VALUE", "PREFIX");
        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [6])]);

        List<Specimen> population =
        [
            new Specimen("AAA111", [new Observation(["VALUE"], [null])]),
            new Specimen("BBB222", [new Observation(["VALUE"], [null])]),
            new Specimen("CCC333", [new Observation(["VALUE"], [null])])
        ];

        // Act
        var result = operation.GetStatistics(specimen, population);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
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
        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [6])]);

        List<Specimen> population =
        [
            new Specimen("AAA111", [new Observation(["VALUE"], [1])]),
            new Specimen("BBB222", [new Observation(["VALUE"], [2])]),
            new Specimen("CCC333", [new Observation(["VALUE"], [null])])
        ];

        // Act
        var result = operation.GetStatistics(specimen, population);

        // Assert
        var populationSizeIndex = result.Names.IndexOf("PREFIX_POPULATION_SIZE");
        var populationSize = result.Values[populationSizeIndex];
        Assert.That(populationSize, Is.EqualTo(2));
    }

    [Test]
    public void GivenDifferenceOption_WhenCalculatingAverageValue_ReturnsDifference()
    {
        // Arrange
        var addValueDifference = true;
        var operation = new AverageOperation("VALUE", "PREFIX", addValueDifference);

        List<Specimen> population =
        [
            new Specimen("AAA111", [new Observation(["VALUE"], [1])]),
            new Specimen("BBB222", [new Observation(["VALUE"], [2])]),
            new Specimen("CCC333", [new Observation(["VALUE"], [3])])
        ];

        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [4])]);

        var returningSpecimen = new ReturningSpecimen(specimen, population);

        // Act
        var result = operation.GetStatistics(returningSpecimen);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "PREFIX_AVERAGE",
            "PREFIX_STANDARD_DEVIATION",
            "PREFIX_POPULATION_SIZE",
            "PREFIX_EMPTY_SIZE",
            "PREFIX_OUTLIER_SIZE",
            "PREFIX_VALUE_DIFFERENCE"
        }));

        var valueDifference = result.Values[result.Names.IndexOf("PREFIX_VALUE_DIFFERENCE")];

        Assert.That(valueDifference, Is.EqualTo(2));
    }

    [Test]
    public void GivenStandardDeviationDifferenceOption_WhenCalculatingAverageValue_ReturnsStandardDeviationDifference()
    {
        // Arrange
        var addValueDifference = false;
        var addStandardDeviationDifference = true;
        var operation = new AverageOperation("VALUE", "PREFIX", addValueDifference, addStandardDeviationDifference);

        List<Specimen> population =
        [
            new Specimen("AAA111", [new Observation(["VALUE"], [8])]),
            new Specimen("BBB222", [new Observation(["VALUE"], [8])]),
            new Specimen("CCC333", [new Observation(["VALUE"], [12])]),
            new Specimen("DDD444", [new Observation(["VALUE"], [12])])
        ];

        var specimen = new Specimen("XXX123", [new Observation(["VALUE"], [4])]);

        var returningSpecimen = new ReturningSpecimen(specimen, population);

        // Act
        var result = operation.GetStatistics(returningSpecimen);

        // Assert
        Assert.That(result.Names, Is.EquivalentTo(new ArrayList
        {
            "PREFIX_AVERAGE",
            "PREFIX_STANDARD_DEVIATION",
            "PREFIX_POPULATION_SIZE",
            "PREFIX_EMPTY_SIZE",
            "PREFIX_OUTLIER_SIZE",
            "PREFIX_STANDARD_DEVIATION_DIFFERENCE"
        }));

        var standardDeviationDifference = result.Values[result.Names.IndexOf("PREFIX_STANDARD_DEVIATION_DIFFERENCE")];

        Assert.That(standardDeviationDifference, Is.EqualTo(-3));
    }
}