using System.Collections;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Processors.Computed.WPF.Symmetry;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Computed.WPF.Tests;

public class SymmetryViewModelTests
{
    private SymmetryModel _model = null!;
    private SymmetryCalculator _parameters = null!;
    private Mock<IDataLabelRepository> _repository = null!;
    private SymmetryViewModel _viewModel = null!;

    [SetUp]
    public void Initialize()
    {
        _repository = new Mock<IDataLabelRepository>();
        _parameters = new SymmetryCalculator();
        _model = new SymmetryModel(_parameters, _repository.Object);
        _viewModel = new SymmetryViewModel(_model);
    }

    [Test]
    public void GivenEmptyParameters_WhenSetValuesInViewModel_ThenParameterValuesAreSet()
    {
        // Arrange
        _parameters.ResultName = string.Empty;
        _parameters.WingParameters = [];
        _parameters.WingName = string.Empty;

        // Act
        _viewModel.ResultName = "SYMMETRY";
        _viewModel.WingName = "WING";
        _viewModel.D2Name = "D2";
        _viewModel.D3Name = "D3";
        _viewModel.D4Name = "D4";
        _viewModel.D5Name = "D5";
        _viewModel.D6Name = "D6";
        _viewModel.D7Name = "D7";
        _viewModel.D8Name = "D8";

        // Assert
        Assert.That(_parameters.ResultName, Is.EqualTo("SYMMETRY"));
        Assert.That(_parameters.WingName, Is.EqualTo("WING"));
        Assert.That(_parameters.WingParameters, Is.EquivalentTo(new ArrayList { "D2", "D3", "D4", "D5", "D6", "D7", "D8" }));
    }

    [Test]
    public void GivenEmptyParameters_WhenResultNameIsSet_ThenRepositoryIsUpdated()
    {
        // Arrange
        _parameters.ResultName = string.Empty;
        _repository.Setup(r => r.AddAdditionalLabel(It.IsAny<DataLabel>())).Verifiable();

        // Act
        _viewModel.ResultName = "SYMMETRY";
        _viewModel.UpdateLabelCommand.Execute(null);

        // Assert
        _repository.Verify(r => r.AddAdditionalLabel(It.IsAny<DataLabel>()), Times.Once);
    }

    [Test]
    public void GivenExistingValue_WhenResultNameIsChanged_ThenRepositoryIsUpdatedAndOldValueIsRemoved()
    {
        // Arrange
        _parameters.ResultName = string.Empty;
        _repository.Setup(r => r.AddAdditionalLabel(It.IsAny<DataLabel>())).Verifiable();

        // Act
        _viewModel.ResultName = "SYMMETRY";
        _viewModel.UpdateLabelCommand.Execute(null);
        _viewModel.ResultName = "SYMMETRY-2";
        _viewModel.UpdateLabelCommand.Execute(null);

        // Assert
        _repository.Verify(r => r.AddAdditionalLabel(It.Is<DataLabel>(x => x.Name == "SYMMETRY")), Times.Once);
        _repository.Verify(r => r.RemoveAdditionalLabel(It.Is<DataLabel>(x => x.Name == "SYMMETRY")), Times.Once);
        _repository.Verify(r => r.AddAdditionalLabel(It.Is<DataLabel>(x => x.Name == "SYMMETRY-2")), Times.Once);
    }

    [Test]
    public void GivenExistingResultName_WhenRemoveViewModel_ThenRepositoryRemovesValue()
    {
        // Arrange
        _viewModel.ResultName = "SYMMETRY";
        _viewModel.UpdateLabelCommand.Execute(null);
        _repository.Setup(r => r.RemoveAdditionalLabel(It.IsAny<DataLabel>())).Verifiable();

        // Act
        _viewModel.RemoveCommand.Execute(null);

        // Assert
        _repository.Verify(r => r.RemoveAdditionalLabel(It.Is<DataLabel>(x => x.Name == "SYMMETRY")), Times.Once);
    }
}