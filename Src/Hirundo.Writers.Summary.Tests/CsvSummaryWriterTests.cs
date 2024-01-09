using System.Text;
using Hirundo.Commons;
using NUnit.Framework;

namespace Hirundo.Writers.Summary.Tests;

public class CsvSummaryWriterTests
{
    private StringWriter _stringWriter = null!;
    private CsvSummaryWriter _writer = null!;

    [SetUp]
    public void Setup()
    {
        _stringWriter = new StringWriter();
        _writer = new CsvSummaryWriter(_stringWriter);
    }

    [Test]
    public void GivenEmptyRecords_WhenWriteSummary_ReturnsEmptyDocument()
    {
        // Arrange
        var returningSpecimen = new Specimen("AB123", []);
        Specimen[] population = [];
        StatisticalData[] statistics = [];

        List<ReturningSpecimenSummary> summary = [new ReturningSpecimenSummary(returningSpecimen, population, statistics)];

        // Act
        _writer.Write(summary);

        // Assert
        var result = _stringWriter.ToString();
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void GivenOneColumn_WhenWriteSummary_ReturnsThisColumn()
    {
        // Arrange
        Observation[] observations = [new Observation(["ID"], [123])];
        var returningSpecimen = new Specimen(123, observations);
        Specimen[] population = [];
        StatisticalData[] statistics = [];

        List<ReturningSpecimenSummary> summary = [new ReturningSpecimenSummary(returningSpecimen, population, statistics)];

        // Act
        _writer.Write(summary);

        // Assert
        var result = _stringWriter.ToString();
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo("ID\r\n123\r\n"));
    }

    [Test]
    public void GivenOnePrimaryColumnAndOneStatisticalColumn_WhenWrite_ReturnsBothColumns()
    {
        // Arrange
        Observation[] observations = [new Observation(["PID"], [123])];
        var returningSpecimen = new Specimen(123, observations);
        Specimen[] population = [];
        StatisticalData[] statistics = [new StatisticalData("MEAN_WEIGHT", 22.5)];

        List<ReturningSpecimenSummary> summary = [new ReturningSpecimenSummary(returningSpecimen, population, statistics)];

        // Act
        _writer.Write(summary);

        // Assert
        var result = _stringWriter.ToString();
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo("PID,MEAN_WEIGHT\r\n123,22.5\r\n"));
    }
}