using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Databases.Helpers;
using Hirundo.Databases.WPF.Excel;
using Moq;
using NUnit.Framework;

namespace Hirundo.Databases.WPF.Tests;

[TestFixture]
public class ExcelDataSourceViewModelTests
{
    private Mock<IExcelMetadataService> _metadataLoader = null!;
    private Mock<ILabelsRepository> _labelsRepository = null!;
    private Mock<ISpeciesRepository> _speciesRepository = null!;

    private ExcelDatabaseParameters _parameters = null!;
    private ExcelDataSourceModel _model = null!;
    private ExcelDataSourceViewModel _viewModel = null!;

    [SetUp]
    public void Setup()
    {
        _parameters = new ExcelDatabaseParameters();

        _metadataLoader = new Mock<IExcelMetadataService>();
        _labelsRepository = new Mock<ILabelsRepository>();
        _speciesRepository = new Mock<ISpeciesRepository>();

        _model = new ExcelDataSourceModel(_parameters, _labelsRepository.Object, _speciesRepository.Object);
        _viewModel = new ExcelDataSourceViewModel(_model, _metadataLoader.Object);
    }

    [Test]
    public void WhenLoadingNewFile_CallsExcelMetadataServiceForColumnList()
    {
        // Arrange
        var path = "test.xlsx";
        _viewModel.Path = path;

        var columns = new List<ColumnParameters>
        {
            new() { DatabaseColumn = "Column1" },
            new() { DatabaseColumn = "Column2" }
        };

        _metadataLoader.Setup(x => x.GetColumns(It.IsAny<string>())).Returns(columns);

        // Act
        _viewModel.LoadFileCommand.Execute(null);

        // Assert
        _metadataLoader.Verify(x => x.GetColumns(It.Is<string>(p => p == path)), Times.Once);
        Assert.That(_viewModel.Columns, Is.EquivalentTo(columns));
        Assert.That(_viewModel.DataColumns, Is.EquivalentTo(columns.Select(c => c.DatabaseColumn)));
        Assert.That(_model.Columns, Is.EquivalentTo(columns));
        Assert.That(_parameters.Columns, Is.EquivalentTo(columns));
    }

    [Test]
    public void WhenLoadingNewFile_LabelsAreUpdated()
    {
        // Arrange
        var path = "test.xlsx";
        _viewModel.Path = path;

        var columns = new List<ColumnParameters>
        {
            new()
            {
                DatabaseColumn = "Column1",
                ValueName = "Value1"
            },
            new()
            {
                DatabaseColumn = "Column2",
                ValueName = "Value2"
            }
        };

        _metadataLoader.Setup(x => x.GetColumns(It.IsAny<string>())).Returns(columns);

        bool isLabelsUpdated = false;
        _viewModel.LabelsUpdated += (a, b) => isLabelsUpdated = true;

        // Act
        _viewModel.LoadFileCommand.Execute(null);

        // Assert
        Assert.That(isLabelsUpdated, Is.True);
        _labelsRepository.Setup(r => r.SetLabels(It.IsAny<IEnumerable<DataLabel>>()));
        var arguments = _labelsRepository.Invocations[0].Arguments[0] as IEnumerable<DataLabel>;
        Assert.That(arguments, Is.InstanceOf<IEnumerable<DataLabel>>());
        Assert.That(arguments!.Select(l => l.Name), Is.EquivalentTo(columns.Select(c => c.ValueName)));
    }
}
