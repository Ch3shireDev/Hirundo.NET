using Hirundo.Commons.Repositories;
using Hirundo.Databases.Helpers;
using Hirundo.Databases.WPF.Access;
using Moq;
using NUnit.Framework;

namespace Hirundo.Databases.WPF.Tests;

[TestFixture]
public class DataSourceModelTests
{
    private DataSourceModel _model = null!;
    private Mock<ILabelsRepository> _labelsRepository = null!;
    private Mock<IAccessMetadataService> _accessMetadataService = null!;
    private Mock<ISpeciesRepository> _speciesRepository = null!;
    private Mock<IExcelMetadataService> _excelMetadataService = null!;

    [SetUp]
    public void Setup()
    {
        _labelsRepository = new Mock<ILabelsRepository>();
        _accessMetadataService = new Mock<IAccessMetadataService>();
        _speciesRepository = new Mock<ISpeciesRepository>();
        _excelMetadataService = new Mock<IExcelMetadataService>();

        _model = new DataSourceModel(_labelsRepository.Object, _speciesRepository.Object, _accessMetadataService.Object, _excelMetadataService.Object);
    }

    [Test]
    public void GivenDataSourceModel_WhenGetParametersViewModels_ReturnsParametersViewModels()
    {
        // Arrange
        var parameters = new AccessDatabaseParameters();
        _model.ParametersContainer.Databases.Add(parameters);

        // Act
        var result = _model.GetParametersViewModels().ToList();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);
        var viewModel = result[0];
        Assert.That(viewModel, Is.Not.Null);
        Assert.That(viewModel, Is.InstanceOf<AccessDataSourceViewModel>());
        Assert.That(viewModel.Name, Is.Not.Empty);
        Assert.That(viewModel.Description, Is.Not.Empty);
    }
}