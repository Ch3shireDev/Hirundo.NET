using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Databases.Helpers;
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
        _repository = new Mock<ILabelsRepository>();
        _parameters = new AccessDatabaseParameters();
        var _speciesRepository = new Mock<ISpeciesRepository>();
        _model = new AccessDataSourceModel(_parameters, _repository.Object, _speciesRepository.Object);
        _metadataService = new Mock<IAccessMetadataService>();
        _viewModel = new AccessDataSourceViewModel(_model, _metadataService.Object);
    }

    private Mock<IAccessMetadataService> _metadataService = null!;
    private Mock<ILabelsRepository> _repository = null!;
    private AccessDataSourceViewModel _viewModel = null!;
    private AccessDataSourceModel _model = null!;
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
        _parameters.Columns.Add(new ColumnParameters("Column1", "Text1", DataType.Text));
        _parameters.Columns.Add(new ColumnParameters("Column2", "Text2", DataType.Text));
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

    [Test]
    public void GivenEmptyParameters_WhenAddCondition_AddsConditionToParameter()
    {
        // Arrange
        _parameters.Conditions.Clear();

        // Act
        _viewModel.AddCondition();

        // Assert
        Assert.That(_parameters.Conditions.Count, Is.EqualTo(1));
        Assert.That(_viewModel.Conditions.Count, Is.EqualTo(1));
    }

    [Test]
    public void GivenExistingCondition_WhenRemoveCondition_RemovesConditionFromParameter()
    {
        // Arrange
        _parameters.Conditions.Clear();
        _viewModel.AddCondition();

        // Act
        _viewModel.RemoveCondition();

        // Assert
        Assert.That(_parameters.Conditions.Count, Is.EqualTo(0));
        Assert.That(_viewModel.Conditions.Count, Is.EqualTo(0));
    }

    [Test]
    public void GivenEmptyColumns_WhenAddColumn_AddsColumnToColumns()
    {
        // Arrange
        _viewModel.Columns.Clear();

        // Act
        _viewModel.AddColumn();

        // Assert
        Assert.That(_viewModel.Columns.Count, Is.EqualTo(1));
        Assert.That(_parameters.Columns.Count, Is.EqualTo(1));
    }

    [Test]
    public void GivenColumns_WhenRemoveColumn_RemovesColumnFromColumns()
    {
        // Arrange
        _viewModel.Columns.Clear();
        _viewModel.AddColumn();
        _viewModel.AddColumn();

        // Act
        _viewModel.RemoveColumn();

        // Assert
        Assert.That(_viewModel.Columns.Count, Is.EqualTo(1));
        Assert.That(_parameters.Columns.Count, Is.EqualTo(1));
    }
}
