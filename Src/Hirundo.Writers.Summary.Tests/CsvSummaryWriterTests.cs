using Hirundo.Commons;
using NUnit.Framework;

namespace Hirundo.Writers.Summary.Tests;

public class CsvSummaryWriterTests
{
    [Test]
    public void GivenEmptyRecords_WhenWriteSummary_ReturnsEmptyDocument()
    {
        // Arrange
        var stringWriter = new StringWriter();
        using var writer = new CsvSummaryWriter(stringWriter);
        var returningSpecimen = new Specimen("AB123", []);
        Specimen[] population = [];
        StatisticalData[] statistics = [];

        List<ReturningSpecimenSummary> summary = [new ReturningSpecimenSummary(returningSpecimen, population, statistics)];

        // Act
        writer.Write(summary);

        // Assert
        var result = stringWriter.ToString();
        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void GivenOneColumn_WhenWriteSummary_ReturnsThisColumn()
    {
        // Arrange
        var stringWriter = new StringWriter();
        using var writer = new CsvSummaryWriter(stringWriter);
        Observation[] observations = [new Observation(["ID"], [123])];
        var returningSpecimen = new Specimen(123, observations);
        Specimen[] population = [];
        StatisticalData[] statistics = [];

        List<ReturningSpecimenSummary> summary = [new ReturningSpecimenSummary(returningSpecimen, population, statistics)];

        // Act
        writer.Write(summary);

        // Assert
        var result = stringWriter.ToString();
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo("ID\r\n123\r\n"));
    }

    [Test]
    public void GivenOnePrimaryColumnAndOneStatisticalColumn_WhenWrite_ReturnsBothColumns()
    {
        // Arrange
        var stringWriter = new StringWriter();
        using var writer = new CsvSummaryWriter(stringWriter);
        Observation[] observations = [new Observation(["PID"], [123])];
        var returningSpecimen = new Specimen(123, observations);
        Specimen[] population = [];
        StatisticalData[] statistics = [new StatisticalData("MEAN_WEIGHT", 22.5)];

        List<ReturningSpecimenSummary> summary = [new ReturningSpecimenSummary(returningSpecimen, population, statistics)];

        // Act
        writer.Write(summary);

        // Assert
        var result = stringWriter.ToString();
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo("PID,MEAN_WEIGHT\r\n123,22.5\r\n"));
    }

    [Test]
    public void GivenTwoRecordsWithDifferentColumns_WhenWrite_CreatedDocumentContainsAllColumns()
    {
        // Arrange
        var stringWriter = new StringWriter();
        using var writer = new CsvSummaryWriter(stringWriter);
        Observation[] observations1 = [new Observation(["PID"], [123])];
        var returningSpecimen1 = new Specimen(123, observations1);
        Observation[] observations2 = [new Observation(["PID", "WEIGHT"], [456, 22.5])];
        var returningSpecimen2 = new Specimen(456, observations2);
        Specimen[] population = [];
        StatisticalData[] statistics = [];

        List<ReturningSpecimenSummary> summary =
        [
            new ReturningSpecimenSummary(returningSpecimen1, population, statistics),
            new ReturningSpecimenSummary(returningSpecimen2, population, statistics)
        ];

        // Act
        writer.Write(summary);

        // Assert
        var result = stringWriter.ToString();
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo("PID,WEIGHT\r\n123,\r\n456,22.5\r\n"));
    }

    [Test]
    public void GivenDifferentStatisticalData_WhenWrite_CreatedDocumentIncludesAllData()
    {
        // Arrange
        var stringWriter = new StringWriter();
        using var writer = new CsvSummaryWriter(stringWriter);
        Observation[] observations1 = [new Observation(["PID"], [123])];
        var returningSpecimen1 = new Specimen(123, observations1);
        Observation[] observations2 = [new Observation(["PID"], [456])];
        var returningSpecimen2 = new Specimen(456, observations2);
        Specimen[] population = [];
        StatisticalData[] statistics1 =
        [
            new StatisticalData("D1", 3)
        ];
        StatisticalData[] statistics2 =
        [
            new StatisticalData("D2", 5)
        ];

        List<ReturningSpecimenSummary> summary =
        [
            new ReturningSpecimenSummary(returningSpecimen1, population, statistics1),
            new ReturningSpecimenSummary(returningSpecimen2, population, statistics2)
        ];

        // Act
        writer.Write(summary);

        // Assert
        var result = stringWriter.ToString();
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo("PID,D1,D2\r\n123,3,\r\n456,,5\r\n"));
    }
}