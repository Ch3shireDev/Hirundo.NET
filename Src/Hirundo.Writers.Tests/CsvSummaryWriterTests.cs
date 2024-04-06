using Hirundo.Commons.Models;
using NUnit.Framework;

namespace Hirundo.Writers.Tests;

[TestFixture]
public class CsvSummaryWriterTests
{
    [Test]
    public void GivenEmptyRecords_WhenWriteSummary_ReturnsEmptyDocument()
    {
        // Arrange
        var stringWriter = new StringWriter();
        using var writer = new CsvSummaryWriter(stringWriter);
        List<ReturningSpecimenSummary> summary = [new ReturningSpecimenSummary([], [])];
        var results = new ReturningSpecimensResults { Results = summary };

        // Act
        writer.Write(results);

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
        List<ReturningSpecimenSummary> summary = [new ReturningSpecimenSummary(["ID"], [123])];
        var results = new ReturningSpecimensResults { Results = summary };

        // Act
        writer.Write(results);

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
        var results = new ReturningSpecimensResults { Results = ([new ReturningSpecimenSummary(["PID", "MEAN_WEIGHT"], [123, 22.5])]) };

        // Act
        writer.Write(results);

        // Assert
        var result = stringWriter.ToString();
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo("PID,MEAN_WEIGHT\r\n123,22.5\r\n"));
    }
}