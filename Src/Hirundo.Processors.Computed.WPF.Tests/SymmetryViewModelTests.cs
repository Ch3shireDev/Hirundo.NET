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
        Assert.That(_parameters.WingParameters, Is.EquivalentTo(new[] { "D2", "D3", "D4", "D5", "D6", "D7", "D8" }));
    }
}