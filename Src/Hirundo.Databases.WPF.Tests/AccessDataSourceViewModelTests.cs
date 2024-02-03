using Hirundo.Databases.WPF.Access;
using Moq;
using NUnit.Framework;

namespace Hirundo.Databases.WPF.Tests;

[TestFixture]
public class AccessDataSourceViewModelTests
{
    [SetUp]
    public void Setup()
    {
        _parameters = new AccessDatabaseParameters();
        _metadataService = new Mock<IAccessMetadataService>();
        _viewModel = new AccessDataSourceViewModel(_parameters, _metadataService.Object);
    }

    private Mock<IAccessMetadataService> _metadataService = null!;
    private AccessDataSourceViewModel _viewModel = null!;
    private AccessDatabaseParameters _parameters = null!;

    [Test]
    public void GivenEmptyParameters_WhenAddColumn_AddsColumnToParameter()
    {
        // Arrange
        _parameters.Columns.Clear();

        // Act
        _viewModel.AddColumn();

        // Assert
        Assert.That(_parameters.Columns.Count, Is.EqualTo(1));
        Assert.That(_viewModel.Columns.Count, Is.EqualTo(1));
    }

    [Test]
    public void GivenColumnsInParameters_WhenRemoveColumns_GetsLessColumns()
    {
        // Arrange
        _parameters.Columns.Clear();
        _parameters.Columns.Add(new ColumnMapping("Column1", "Text1", DataValueType.Text));
        _parameters.Columns.Add(new ColumnMapping("Column2", "Text2", DataValueType.Text));
        _viewModel.Columns.Add(_parameters.Columns[0]);
        _viewModel.Columns.Add(_parameters.Columns[1]);

        // Act
        _viewModel.RemoveColumn();

        // Assert
        Assert.That(_parameters.Columns.Count, Is.EqualTo(1));
        Assert.That(_viewModel.Columns.Count, Is.EqualTo(1));
        Assert.That(_viewModel.Columns[0].DatabaseColumn, Is.EqualTo("Column1"));
        Assert.That(_viewModel.Columns[0].ValueName, Is.EqualTo("Text1"));
        Assert.That(_parameters.Columns[0].DatabaseColumn, Is.EqualTo("Column1"));
        Assert.That(_parameters.Columns[0].ValueName, Is.EqualTo("Text1"));
    }
}