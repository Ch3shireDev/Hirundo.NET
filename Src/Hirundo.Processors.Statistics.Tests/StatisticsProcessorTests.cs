using Hirundo.Commons.Models;
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

        processor = new StatisticsProcessor(
            [
                operation1.Object,
                operation2.Object
            ]
        );
    }

    [Test]
    public void GivenOperations_WhenGetStatistics_ReturnsSetOfStatisticalData()
    {
        // Arrange
        operation1
            .Setup(x => x.GetStatistics(It.IsAny<Specimen>(), It.IsAny<IEnumerable<Specimen>>()))
            .Returns(new StatisticalOperationResult("OPERATION1", 1));

        operation2
            .Setup(x => x.GetStatistics(It.IsAny<Specimen>(), It.IsAny<IEnumerable<Specimen>>()))
            .Returns(new StatisticalOperationResult("OPERATION2", 2));

        var population = new[]
        {
            new Specimen("1", [new Observation(["Value"], [1])]),
            new Specimen("2", [new Observation(["OPERATION2"], [2])])
        };

        var specimen = new Specimen("3", [new Observation(["OPERATION1"], [3])]);

        // Act
        var result = processor.GetStatistics(specimen, population).ToArray();

        // Assert
        Assert.That(result, Has.Length.EqualTo(2));
        Assert.That(result[0].Name, Is.EqualTo("OPERATION1"));
        Assert.That(result[0].Value, Is.EqualTo(1));
        Assert.That(result[1].Name, Is.EqualTo("OPERATION2"));
        Assert.That(result[1].Value, Is.EqualTo(2));
    }
}