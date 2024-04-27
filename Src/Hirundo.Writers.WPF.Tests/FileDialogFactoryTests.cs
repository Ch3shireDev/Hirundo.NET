using Hirundo.Commons.Helpers;
using Moq;
using NUnit.Framework;

namespace Hirundo.Writers.WPF.Tests;

[TestFixture]
public class FileDialogFactoryTests
{
    private Mock<IFileDialogHelper> fileDialogHelper = null!;

    [SetUp]
    public void Initialize()
    {
        fileDialogHelper = new Mock<IFileDialogHelper>();
        FileHelpers.File = fileDialogHelper.Object;
    }

    [Test]
    public void GetFileDialogForWriter_GivenCsvWriter_ReturnsWriterDialog()
    {
        // Arrange
        var writerParameters = new CsvSummaryWriterParameters();
        var directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        FileDialogFactory.DefaultDirectory = directory;
        FileDialogFactory.DefaultResultsName = "wyniki";

        // Act
        var dialog = FileDialogFactory.GetFileDialogForWriter(writerParameters);

        // Assert
        Assert.That(dialog.Filter, Is.EqualTo("Pliki CSV (*.csv)|*.csv"));
        Assert.That(dialog.Title, Is.EqualTo("Zapisz wyniki"));
        Assert.That(dialog.DefaultExt, Is.EqualTo("csv"));
        Assert.That(dialog.FileName, Is.EqualTo("wyniki.csv"));
        Assert.That(dialog.DefaultDirectory, Is.EqualTo(directory));
    }

    [Test]
    public void GetFileDialogForWriter_GivenXlsxWriter_ReturnsWriterDialog()
    {
        // Arrange
        var writerParameters = new XlsxSummaryWriterParameters();
        var directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        FileDialogFactory.DefaultDirectory = directory;
        FileDialogFactory.DefaultResultsName = "wyniki";

        // Act
        var dialog = FileDialogFactory.GetFileDialogForWriter(writerParameters);

        // Assert
        Assert.That(dialog.Filter, Is.EqualTo("Pliki Excel (*.xlsx)|*.xlsx"));
        Assert.That(dialog.Title, Is.EqualTo("Zapisz wyniki"));
        Assert.That(dialog.DefaultExt, Is.EqualTo("xlsx"));
        Assert.That(dialog.FileName, Is.EqualTo("wyniki.xlsx"));
        Assert.That(dialog.DefaultDirectory, Is.EqualTo(directory));
    }

    [Test]
    public void GetFileDialogForWriter_IfFileAlreadyExists_ReturnsOtherFileName()
    {
        // Arrange
        var writerParameters = new CsvSummaryWriterParameters();
        var directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        FileDialogFactory.DefaultDirectory = directory;
        FileDialogFactory.DefaultResultsName = "wyniki";

        var filePath1 = Path.Combine(directory, "wyniki.csv");
        var filePath2 = Path.Combine(directory, "wyniki (1).csv");

        fileDialogHelper.Setup(x => x.Exists(It.Is<string>(fp => fp == filePath1))).Returns(true);
        fileDialogHelper.Setup(x => x.Exists(It.Is<string>(fp => fp == filePath2))).Returns(false);

        // Act
        var dialog = FileDialogFactory.GetFileDialogForWriter(writerParameters);

        // Assert
        Assert.That(dialog.FileName, Is.EqualTo("wyniki (1).csv"));
    }

    [Test]
    public void GetFileDialogForWriter_IfFileAlreadyExists_ReturnsFirstAvailableFilename()
    {
        // Arrange
        var writerParameters = new XlsxSummaryWriterParameters();
        var directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        FileDialogFactory.DefaultDirectory = directory;
        FileDialogFactory.DefaultResultsName = "wyniki";

        var filePath1 = Path.Combine(directory, "wyniki.xlsx");
        var filePath2 = Path.Combine(directory, "wyniki (1).xlsx");
        var filePath3 = Path.Combine(directory, "wyniki (2).xlsx");

        fileDialogHelper.Setup(x => x.Exists(It.Is<string>(fp => fp == filePath1))).Returns(true);
        fileDialogHelper.Setup(x => x.Exists(It.Is<string>(fp => fp == filePath2))).Returns(true);
        fileDialogHelper.Setup(x => x.Exists(It.Is<string>(fp => fp == filePath3))).Returns(false);

        // Act
        var dialog = FileDialogFactory.GetFileDialogForWriter(writerParameters);

        // Assert
        Assert.That(dialog.FileName, Is.EqualTo("wyniki (2).xlsx"));
    }
}
