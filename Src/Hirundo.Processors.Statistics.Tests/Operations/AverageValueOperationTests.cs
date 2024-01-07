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
        var populationData = new PopulationData
        {
            Specimens =
            {
                new Specimen
                {
                    Identifier = "ABC123",
                    Observations =
                    {
                        new Observation(["VALUE"], [2]),
                    }
                },
                new Specimen
                {
                    Identifier = "DEF456",
                    Observations =
                    {
                        new Observation(["VALUE"], [1]),
                    }
                },
                new Specimen
                {
                    Identifier = "GHI789",
                    Observations =
                    {
                        new Observation(["VALUE"], [6]),
                    }
                }
            }
        };

        var operation = new AverageValueOperation("VALUE", "AVERAGE_VALUE");

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Name, Is.EqualTo("AVERAGE_VALUE"));
        Assert.That(result.Value, Is.EqualTo(3));
    }

    [Test]
    public void GivenNullsInValues_WhenGetStatistics_ReturnsAverageOverGivenValue()
    {
        // Arrange
        var populationData = new PopulationData
        {
            Specimens =
            {
                new Specimen
                {
                    Identifier = "ABC123",
                    Observations =
                    {
                        new Observation(["VALUE"], [2]),
                    }
                },
                new Specimen
                {
                    Identifier = "DEF456",
                    Observations =
                    {
                        new Observation(["VALUE"], [1]),
                    }
                },
                new Specimen
                {
                    Identifier = "GHI789",
                    Observations =
                    {
                        new Observation(["VALUE"], [6]),
                    }
                },
                new Specimen
                {
                    Identifier = "JKL012",
                    Observations =
                    {
                        new Observation(["VALUE"], [null]),
                    }
                }
            }
        };

        var operation = new AverageValueOperation("VALUE", "AVERAGE_VALUE");

        // Act
        var result = operation.GetStatistics(populationData);

        // Assert
        Assert.That(result.Name, Is.EqualTo("AVERAGE_VALUE"));
        Assert.That(result.Value, Is.EqualTo(3));
    }
}