using Hirundo.Commons.Models;
using NUnit.Framework;

namespace Hirundo.Writers.Tests;

[TestFixture]
public class CsvSummaryWriterTests
{
    private CsvSummaryWriterParameters _parameters = null!;
    private StringWriter stringWriter = null!;
    private CsvSummaryWriter writer = null!;

    [SetUp]
    public void Initialize()
    {
        _parameters = new CsvSummaryWriterParameters
        {
            Path = "test.csv",
            RingHeaderName = "Ring",
            DateFirstSeenHeaderName = "DateFirstSeen",
            DateLastSeenHeaderName = "DateLastSeen"
        };
        stringWriter = new StringWriter();
        writer = new CsvSummaryWriter(_parameters, stringWriter);
    }

    [Test]
    public void GivenEmptyRecords_WhenWriteSummary_ReturnsEmptyDocument()
    {
        // Arrange
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
        List<ReturningSpecimenSummary> summary = [new ReturningSpecimenSummary(["ID"], [123])];
        var results = new ReturningSpecimensResults { Results = summary };

        // Act
        writer.Write(results);

        // Assert
        var result = stringWriter.ToString();
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo("Ring,DateFirstSeen,DateLastSeen,ID\r\n123,2020-06-01,2021-06-01,123\r\n"));
    }

    [Test]
    public void GivenOnePrimaryColumnAndOneStatisticalColumn_WhenWrite_ReturnsBothColumns()
    {
        // Arrange
        var results = new ReturningSpecimensResults { Results = [new ReturningSpecimenSummary(["PID", "MEAN_WEIGHT"], [123, 22.5])] };

        // Act
        writer.Write(results);

        // Assert
        var result = stringWriter.ToString();
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo("Ring,DateFirstSeen,DateLastSeen,PID,MEAN_WEIGHT\r\n123,2020-06-01,2021-06-01,123,22.5\r\n"));
    }
}