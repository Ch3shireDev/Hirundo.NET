using Hirundo.Commons;
using Hirundo.Processors.Statistics.Operations;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Statistics.Tests;

public class StatisticsProcessorTests
{
    private Mock<IStatisticalOperation> operation1 = null!;
    private Mock<IStatisticalOperation> operation2 = null!;
    private StatisticsProcessor processor = null!;

    [SetUp]
    public void Setup()
    {
        operation1 = new Mock<IStatisticalOperation>();
        operation2 = new Mock<IStatisticalOperation>();
        processor = new StatisticsProcessor(operation1.Object, operation2.Object);
    }

    [Test]
    public void GivenOperations_WhenGetStatistics_ReturnsSetOfStatisticalData()
    {
        // Arrange
        operation1
            .Setup(x => x.GetStatistics(It.IsAny<PopulationData>()))
            .Returns(new StatisticalDataValue("OPERATION1", 1));

        operation2
            .Setup(x => x.GetStatistics(It.IsAny<PopulationData>()))
            .Returns(new StatisticalDataValue("OPERATION2", 2));

        var populationData = new PopulationData();

        // Act
        var result = processor.GetStatistics(populationData);

        // Assert
        Assert.That(result.Values, Has.Length.EqualTo(2));
        Assert.That(result.Values[0].Name, Is.EqualTo("OPERATION1"));
        Assert.That(result.Values[0].Value, Is.EqualTo(1));
        Assert.That(result.Values[1].Name, Is.EqualTo("OPERATION2"));
        Assert.That(result.Values[1].Value, Is.EqualTo(2));
    }
}