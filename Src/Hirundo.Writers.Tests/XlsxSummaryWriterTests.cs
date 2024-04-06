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

    [SetUp]
    public void Initialize()
    {
        _cancellationToken = new CancellationToken();
        _stream = new MemoryStream();
        _streamWriter = new StreamWriter(_stream);
        _writer = new XlsxSummaryWriter(_streamWriter, _cancellationToken);
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

        _writer.IncludeExplanation = true;

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

        // Act
        _writer.Write(results);

        // Assert
        var xlsx = new XLWorkbook(_stream);
        var worksheet = xlsx.Worksheets.Worksheet(1);
        Assert.That(worksheet.Cell(1, 1).Value, Is.EqualTo("COL1"));
        Assert.That(worksheet.Cell(2, 1).Value, Is.EqualTo("ROW1"));
    }

    [Test]
    public void GivenNumberInData_WhenWrite_CreatesNumberInsteadOfText()
    {
        // Arrange
        var summary = new ReturningSpecimenSummary(["COL1"], [1]);
        var results = new ReturningSpecimensResults(summary);

        // Act
        _writer.Write(results);

        // Assert
        var xlsx = new XLWorkbook(_stream);
        var worksheet = xlsx.Worksheets.Worksheet(1);
        Assert.That(worksheet.Cell(2, 1).Value, Is.EqualTo(1));
    }

    [Test]
    public void GivenTitle_WhenWrite_AddsTitleToSpreadsheet()
    {
        // Arrange
        var summary = new ReturningSpecimenSummary(["COL1"], [1]);
        var results = new ReturningSpecimensResults(summary);

        _writer.Title = "Zestawienie dla XXX";

        // Act
        _writer.Write(results);

        // Assert
        var xlsx = new XLWorkbook(_stream);
        var worksheet = xlsx.Worksheets.Worksheet(1);
        Assert.That(worksheet.Name, Is.EqualTo("Summary"));
        Assert.That(worksheet.Cell(1, 1).Value, Is.EqualTo("Zestawienie dla XXX"));
        Assert.That(worksheet.Cell(3, 1).Value, Is.EqualTo("COL1"));
        Assert.That(worksheet.Cell(4, 1).Value, Is.EqualTo(1));
    }

    [Test]
    public void GivenSubtitle_WhenWrite_AddsSubtitleToSpreadsheet()
    {
        // Arrange
        var summary = new ReturningSpecimenSummary(["COL1"], [1]);
        var results = new ReturningSpecimensResults(summary);

        _writer.Subtitle = "Dodatkowy opis dla XXX";

        // Act
        _writer.Write(results);

        // Assert
        var xlsx = new XLWorkbook(_stream);
        var worksheet = xlsx.Worksheets.Worksheet(1);
        Assert.That(worksheet.Name, Is.EqualTo("Summary"));
        Assert.That(worksheet.Cell(1, 1).Value, Is.EqualTo("Dodatkowy opis dla XXX"));
        Assert.That(worksheet.Cell(3, 1).Value, Is.EqualTo("COL1"));
        Assert.That(worksheet.Cell(4, 1).Value, Is.EqualTo(1));
    }

    [Test]
    public void GivenTitleAndSubtitle_WhenWrite_AddsBothToSpreadsheet()
    {
        // Arrange
        var summary = new ReturningSpecimenSummary(["COL1"], [1]);
        var results = new ReturningSpecimensResults(summary);

        _writer.Title = "Tytuł";
        _writer.Subtitle = "Podtytuł";

        // Act
        _writer.Write(results);

        // Assert
        var xlsx = new XLWorkbook(_stream);
        var worksheet = xlsx.Worksheets.Worksheet(1);
        Assert.That(worksheet.Name, Is.EqualTo("Summary"));
        Assert.That(worksheet.Cell(1, 1).Value, Is.EqualTo("Tytuł"));
        Assert.That(worksheet.Cell(2, 1).Value, Is.EqualTo("Podtytuł"));
        Assert.That(worksheet.Cell(4, 1).Value, Is.EqualTo("COL1"));
        Assert.That(worksheet.Cell(5, 1).Value, Is.EqualTo(1));
    }
}
