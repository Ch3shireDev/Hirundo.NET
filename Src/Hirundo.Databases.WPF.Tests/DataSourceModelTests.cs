using Hirundo.Commons.Repositories.Labels;
using Hirundo.Databases.WPF.Access;

using Moq;
using NUnit.Framework;

namespace Hirundo.Databases.WPF.Tests;

public class DataSourceModelTests
{
    private DataSourceModel _model = null!;
    private Mock<IDataLabelRepository> _repository = null!;
    private Mock<IAccessMetadataService> _metadataService = null!;

    [SetUp]
    public void Setup()
    {
        _repository = new Mock<IDataLabelRepository>();
        _metadataService = new Mock<IAccessMetadataService>();
        _model = new DataSourceModel(_repository.Object, _metadataService.Object);
    }

    [Test]
    public void GivenAccessDataSource_WhenColumnUpdate_UpdatesRepository()
    {
        // Arrange
        _model.AddDatasource(typeof(AccessDatabaseParameters));
        var viewModels = _model.GetParametersViewModels();
        var viewModel = viewModels.First() as AccessDataSourceViewModel;

        // Act
        viewModel?.UpdateLabelsCommand.Execute(null);

        // Assert
        _repository.Verify(r => r.UpdateLabels(It.IsAny<IList<DataLabel>>()), Times.Once);
    }

    [Test]
    public void GivenTwoAccessDataSources_WhenColumnUpdate_UpdatesRepositoryOnce()
    {
        // Arrange
        _model.AddDatasource(typeof(AccessDatabaseParameters));
        _model.AddDatasource(typeof(AccessDatabaseParameters));
        var viewModels = _model.GetParametersViewModels();
        var viewModel = viewModels.First() as AccessDataSourceViewModel;

        // Act
        viewModel?.UpdateLabelsCommand.Execute(null);

        // Assert
        _repository.Verify(r => r.UpdateLabels(It.IsAny<IList<DataLabel>>()), Times.Once);
    }
}