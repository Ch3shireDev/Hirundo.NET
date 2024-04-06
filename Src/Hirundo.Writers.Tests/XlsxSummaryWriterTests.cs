using ClosedXML.Excel;
using Hirundo.Commons.Models;
using NUnit.Framework;

namespace Hirundo.Writers.Tests;

[TestFixture]
public class XlsxSummaryWriterTests
{
    private MemoryStream _stream = null!;
    private StreamWriter _streamWriter = null!;
    private XlsxSummaryWriter _writer = null!;
    private CancellationToken _cancellationToken;
    private XlsxSummaryWriterParameters _parameters = null!;

    [SetUp]
    public void Initialize()
    {
        _parameters = new XlsxSummaryWriterParameters();
        _cancellationToken = new CancellationToken();
        _stream = new MemoryStream();
        _streamWriter = new StreamWriter(_stream);
        _writer = new XlsxSummaryWriter(_parameters, _streamWriter, _cancellationToken);
    }

    [Test]
    public void GivenEmptyData_WhenWrite_CreatesEmptyXlsx()
    {
        // Arrange
        var results = new ReturningSpecimensResults();

        // Act
        _writer.Write(results);

        // Assert
        var xlsx = new XLWorkbook(_stream);
        Assert.That(xlsx.Worksheets.Count, Is.EqualTo(1));
    }

    [Test]
    public void GivenExplanation_WhenWrite_CreatesSpreadsheetWithExplanation()
    {
        // Arrange
        var results = new ReturningSpecimensResults
        {
            Explanation = "This is an explanation\r\nIt is multiline."
        };

        _parameters.IncludeExplanation = true;

        // Act
        _writer.Write(results);

        // Assert
        var xlsx = new XLWorkbook(_stream);
        Assert.That(xlsx.Worksheets.Count, Is.EqualTo(2));
        var worksheet = xlsx.Worksheets.Worksheet(2);
        Assert.That(worksheet.Name, Is.EqualTo("Explanation"));
        Assert.That(worksheet.Cell(1, 1).Value, Is.EqualTo("This is an explanation"));
        Assert.That(worksheet.Cell(2, 1).Value, Is.EqualTo("It is multiline."));
    }

    [Test]
    public void GivenReturningSpecimen_WhenWrite_CreatesSpreadsheetWithHeaderAndRecord()
    {
        // Arrange
        var summary = new ReturningSpecimenSummary(["COL1"], ["ROW1"]);
        var results = new ReturningSpecimensResults(summary);
        _parameters.RingHeaderName = "RING";
        _parameters.DateFirstSeenHeaderName = "FIRST";
        _parameters.DateLastSeenHeaderName = "LAST";
        _parameters.SpreadsheetTitle = "";
        _parameters.SpreadsheetSubtitle = "";

        // Act
        _writer.Write(results);

        // Assert
        var xlsx = new XLWorkbook(_stream);
        var worksheet = xlsx.Worksheets.Worksheet(1);
        Assert.That(worksheet.Cell(1, 4).Value, Is.EqualTo("COL1"));
        Assert.That(worksheet.Cell(2, 4).Value, Is.EqualTo("ROW1"));
    }

    [Test]
    public void GivenNumberInData_WhenWrite_CreatesNumberInsteadOfText()
    {
        // Arrange
        var summary = new ReturningSpecimenSummary(["COL1"], [1])
        {
            Ring = "CC123",
            DateFirstSeen = new DateTime(2023, 7, 1),
            DateLastSeen = new DateTime(2023, 8, 2)
        };
        var results = new ReturningSpecimensResults(summary);

        _parameters.RingHeaderName = "RING";
        _parameters.DateFirstSeenHeaderName = "FIRST";
        _parameters.DateLastSeenHeaderName = "LAST";
        _parameters.SpreadsheetTitle = "";
        _parameters.SpreadsheetSubtitle = "";

        // Act
        _writer.Write(results);

        // Assert
        var xlsx = new XLWorkbook(_stream);
        var worksheet = xlsx.Worksheets.Worksheet(1);
        Assert.That(worksheet.Cell(2, 4).Value, Is.EqualTo(1));
    }

    [Test]
    public void GivenTitle_WhenWrite_AddsTitleToSpreadsheet()
    {
        // Arrange
        var summary = new ReturningSpecimenSummary(["COL1"], [1])
        {
            Ring = "XY123",
            DateFirstSeen = new DateTime(2023, 7, 1),
            DateLastSeen = new DateTime(2023, 8, 2)
        };

        var results = new ReturningSpecimensResults(summary);

        _parameters.SpreadsheetTitle = "Zestawienie dla XXX";
        _parameters.SpreadsheetSubtitle = "";
        _parameters.RingHeaderName = "RING-2";
        _parameters.DateFirstSeenHeaderName = "FIRST-2";
        _parameters.DateLastSeenHeaderName = "LAST-2";

        // Act
        _writer.Write(results);

        // Assert
        var xlsx = new XLWorkbook(_stream);
        var worksheet = xlsx.Worksheets.Worksheet(1);

        Assert.That(worksheet.Name, Is.EqualTo("Summary"));
        Assert.That(worksheet.Cell(1, 1).Value, Is.EqualTo("Zestawienie dla XXX"));

        Assert.That(worksheet.Cell(3, 1).Value, Is.EqualTo("RING-2"));
        Assert.That(worksheet.Cell(3, 2).Value, Is.EqualTo("FIRST-2"));
        Assert.That(worksheet.Cell(3, 3).Value, Is.EqualTo("LAST-2"));
        Assert.That(worksheet.Cell(3, 4).Value, Is.EqualTo("COL1"));

        Assert.That(worksheet.Cell(4, 1).Value, Is.EqualTo("XY123"));
        Assert.That(worksheet.Cell(4, 2).Value, Is.EqualTo(new DateTime(2023, 07, 01)));
        Assert.That(worksheet.Cell(4, 3).Value, Is.EqualTo(new DateTime(2023, 08, 02)));
        Assert.That(worksheet.Cell(4, 4).Value, Is.EqualTo(1));
    }

    [Test]
    public void GivenSubtitle_WhenWrite_AddsSubtitleToSpreadsheet()
    {
        // Arrange
        var summary = new ReturningSpecimenSummary(["COL1"], [1])
        {
            Ring = "XYZ123",
            DateFirstSeen = new DateTime(2022, 6, 1),
            DateLastSeen = new DateTime(2023, 6, 2)
        };
        var results = new ReturningSpecimensResults(summary);

        _parameters.SpreadsheetTitle = "";
        _parameters.SpreadsheetSubtitle = "Dodatkowy opis dla XXX";
        _parameters.RingHeaderName = "RING-3";
        _parameters.DateFirstSeenHeaderName = "FIRST-3";
        _parameters.DateLastSeenHeaderName = "LAST-3";

        // Act
        _writer.Write(results);

        // Assert
        var xlsx = new XLWorkbook(_stream);
        var worksheet = xlsx.Worksheets.Worksheet(1);
        Assert.That(worksheet.Name, Is.EqualTo("Summary"));

        Assert.That(worksheet.Cell(1, 1).Value, Is.EqualTo("Dodatkowy opis dla XXX"));

        Assert.That(worksheet.Cell(3, 1).Value, Is.EqualTo("RING-3"));
        Assert.That(worksheet.Cell(3, 2).Value, Is.EqualTo("FIRST-3"));
        Assert.That(worksheet.Cell(3, 3).Value, Is.EqualTo("LAST-3"));
        Assert.That(worksheet.Cell(3, 4).Value, Is.EqualTo("COL1"));

        Assert.That(worksheet.Cell(4, 1).Value, Is.EqualTo("XYZ123"));
        Assert.That(worksheet.Cell(4, 2).Value, Is.EqualTo(new DateTime(2022, 06, 01)));
        Assert.That(worksheet.Cell(4, 3).Value, Is.EqualTo(new DateTime(2023, 06, 02)));
        Assert.That(worksheet.Cell(4, 4).Value, Is.EqualTo(1));
    }

    [Test]
    public void GivenTitleAndSubtitle_WhenWrite_AddsBothToSpreadsheet()
    {
        // Arrange
        var summary = new ReturningSpecimenSummary(["COL1"], [1])
        {
            Ring = "AB123",
            DateFirstSeen = new DateTime(2021, 6, 1),
            DateLastSeen = new DateTime(2021, 6, 2)
        };
        var results = new ReturningSpecimensResults(summary);

        _parameters.SpreadsheetTitle = "Tytuł";
        _parameters.SpreadsheetSubtitle = "Podtytuł";
        _parameters.RingHeaderName = "RING";
        _parameters.DateFirstSeenHeaderName = "FIRST";
        _parameters.DateLastSeenHeaderName = "LAST";

        // Act
        _writer.Write(results);

        // Assert
        var xlsx = new XLWorkbook(_stream);
        var worksheet = xlsx.Worksheets.Worksheet(1);

        Assert.That(worksheet.Name, Is.EqualTo("Summary"));
        Assert.That(worksheet.Cell(1, 1).Value, Is.EqualTo("Tytuł"));
        Assert.That(worksheet.Cell(2, 1).Value, Is.EqualTo("Podtytuł"));

        Assert.That(worksheet.Cell(4, 1).Value, Is.EqualTo("RING"));
        Assert.That(worksheet.Cell(4, 2).Value, Is.EqualTo("FIRST"));
        Assert.That(worksheet.Cell(4, 3).Value, Is.EqualTo("LAST"));
        Assert.That(worksheet.Cell(4, 4).Value, Is.EqualTo("COL1"));

        Assert.That(worksheet.Cell(5, 1).Value, Is.EqualTo("AB123"));
        Assert.That(worksheet.Cell(5, 2).Value, Is.EqualTo(new DateTime(2021, 06, 01)));
        Assert.That(worksheet.Cell(5, 3).Value, Is.EqualTo(new DateTime(2021, 06, 02)));
        Assert.That(worksheet.Cell(5, 4).Value, Is.EqualTo(1));
    }
}
