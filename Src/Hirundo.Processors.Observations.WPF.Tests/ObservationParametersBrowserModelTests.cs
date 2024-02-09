using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations.Conditions;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.WPF.Tests;

[TestFixture]
public class ObservationParametersBrowserModelTests
{
    [SetUp]
    public void Initialize()
    {
        _factory = new Mock<IObservationParametersFactory>();
        _model = new ObservationParametersBrowserModel(_factory.Object);
    }

    private ObservationParametersBrowserModel _model = null!;
    private Mock<IObservationParametersFactory> _factory = null!;

    [Test]
    public void GivenEmptyModel_WhenGetParametersDataList_ReturnsInfoAboutAvailableConditions()
    {
        // Arrange
        _factory.Setup(f => f.GetParametersData()).Returns([
            new ParametersData(),
            new ParametersData()
        ]);

        // Act
        var parametersDataList = _model.ParametersDataList;

        // Assert
        Assert.That(parametersDataList.Count, Is.EqualTo(2));
    }

    [Test]
    public void GivenEmptyModel_WhenAddParameters_AddsParameterToObservationParameters()
    {
        // Arrange
        _factory.Setup(f => f.CreateCondition(It.IsAny<ParametersData>())).Returns(new IsNotEmptyCondition());

        // Act
        _model.AddParameters(new ParametersData());

        // Assert
        Assert.That(_model.ObservationsParameters.Conditions.Count, Is.EqualTo(1));
        Assert.That(_model.ObservationsParameters.Conditions[0], Is.InstanceOf<IsNotEmptyCondition>());
    }

    [Test]
    public void GivenEmptyModel_WhenGetParametersViewModels_ReturnsViewModelsForObservationParameters()
    {
        // Arrange
        var viewModel = new Mock<ParametersViewModel>();

        var condition = new Mock<IObservationCondition>();

        _model.ObservationsParameters.Conditions.Add(condition.Object);
        _factory.Setup(f => f.CreateViewModel(It.IsAny<IObservationCondition>())).Returns(viewModel.Object);

        // Act
        var parametersViewModels = _model.GetParametersViewModels().ToArray();

        // Assert
        Assert.That(parametersViewModels, Is.Not.Null);
        Assert.That(parametersViewModels, Is.Not.Empty);
        Assert.That(parametersViewModels, Has.Member(viewModel.Object));
    }
}