using Hirundo.Commons.WPF;
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

        _model = new ObservationParametersBrowserModel(_factory.Object);

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
}