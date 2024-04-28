using Hirundo.Commons.Models;
using Hirundo.Processors.Population;
using Hirundo.Processors.Statistics;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Summary.Tests;

public class SummaryTests
{
    private Mock<IPopulationProcessor> _populationProcessor = null!;
    private SummaryProcessor _processor = null!;
    private Mock<IStatisticsProcessor> _statisticsProcessor = null!;
    private List<Specimen> _totalSpecimens = null!;

    [SetUp]
    public void Setup()
    {
        _totalSpecimens = [];
        _populationProcessor = new Mock<IPopulationProcessor>();
        _statisticsProcessor = new Mock<IStatisticsProcessor>();
        _processor = new SummaryProcessor(_totalSpecimens, _populationProcessor.Object, _statisticsProcessor.Object);
    }

    [Test]
    public void GivenNoValues_WhenGetSummary_ReturnsEmptyHeadersAndValues()
    {
        // Arrange
        _totalSpecimens.Clear();
        _populationProcessor.Setup(p => p.GetPopulation(It.IsAny<Specimen>(), It.IsAny<IEnumerable<Specimen>>())).Returns([]);
        _statisticsProcessor.Setup(p => p.GetStatistics(It.IsAny<Specimen>(), It.IsAny<IEnumerable<Specimen>>())).Returns([]);

        var firstObservation = new Observation { Ring = "123", Date = new DateTime(2020, 06, 01) };
        var lastObservation = new Observation { Ring = "123", Date = new DateTime(2021, 06, 01) };
        var specimen = new Specimen("123", [firstObservation, lastObservation]);

        // Act
        var summary = _processor.GetSummary(specimen);

        // Assert
        Assert.That(summary.Ring, Is.EqualTo("123"));
        Assert.That(summary.DateFirstSeen, Is.EqualTo(new DateTime(2020, 06, 01)));
        Assert.That(summary.DateLastSeen, Is.EqualTo(new DateTime(2021, 06, 01)));
        Assert.That(summary.Headers, Is.Empty);
        Assert.That(summary.Values, Is.Empty);
    }

    [Test]
    public void GivenHeadersAndValues_WhenGetSummary_ReturnsHeadersAndValues()
    {
        // Arrange
        _totalSpecimens.Clear();
        _populationProcessor.Setup(p => p.GetPopulation(It.IsAny<Specimen>(), It.IsAny<IEnumerable<Specimen>>())).Returns([]);

        _statisticsProcessor.Setup(p => p.GetStatistics(It.IsAny<Specimen>(), It.IsAny<IEnumerable<Specimen>>())).Returns([
            new StatisticalData("STATISTICAL_DATA", 123.45M)
        ]);

        var firstObservation = new Observation { Ring = "123", Date = new DateTime(2020, 06, 01), Headers = ["DATA"], Values = ["XYZ"] };
        var lastObservation = new Observation { Ring = "123", Date = new DateTime(2021, 06, 01), Headers = ["DATA"], Values = ["ABC"] };
        var specimen = new Specimen("123", [firstObservation, lastObservation]);

        // Act
        var summary = _processor.GetSummary(specimen);

        // Assert
        Assert.That(summary.Ring, Is.EqualTo("123"));
        Assert.That(summary.DateFirstSeen, Is.EqualTo(new DateTime(2020, 06, 01)));
        Assert.That(summary.DateLastSeen, Is.EqualTo(new DateTime(2021, 06, 01)));
        var expectedHeaders = new[] { "DATA", "STATISTICAL_DATA" };
        Assert.That(summary.Headers, Is.EquivalentTo(expectedHeaders));
        var expectedValues = new object?[] { "XYZ", 123.45M };
        Assert.That(summary.Values, Is.EquivalentTo(expectedValues));
    }
}