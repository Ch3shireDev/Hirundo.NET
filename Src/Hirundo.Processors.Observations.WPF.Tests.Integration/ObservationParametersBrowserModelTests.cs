using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.WPF.Tests.Integration;

[TestFixture]
public class ObservationParametersBrowserModelTests
{
    [SetUp]
    public void Initialize()
    {
        var repository = new Mock<IDataLabelRepository>();
        var factory = new ObservationParametersFactory(repository.Object);
        _model = new ObservationParametersBrowserModel(factory);
    }

    private ObservationParametersBrowserModel _model = null!;

    [Test]
    [TestCase(typeof(IsNotEmptyCondition))]
    [TestCase(typeof(IsEqualCondition))]
    [TestCase(typeof(IsInTimeBlockCondition))]
    [TestCase(typeof(IsInSetCondition))]
    //[TestCase(typeof(IsInSeasonCondition))]
    public void GivenModelWithIsEqual_WhenAddParameters_AddsObservationParameter(Type conditionType)
    {
        // Arrange
        _model.ParametersContainer.Conditions.Clear();

        // Act
        _model.AddParameters(new ParametersData
        {
            ConditionType = conditionType
        });

        // Assert
        Assert.That(_model.ParametersContainer.Conditions.Count, Is.EqualTo(1));
        Assert.That(_model.ParametersContainer.Conditions[0], Is.InstanceOf(conditionType));
    }

    [Test]
    [TestCase(typeof(IsNotEmptyCondition))]
    [TestCase(typeof(IsEqualCondition))]
    [TestCase(typeof(IsInTimeBlockCondition))]
    [TestCase(typeof(IsInSetCondition))]
    public void GivenModelWithIsEqual_WhenRemoveIsEqualViewModel_RemovesIsEqualViewModel(Type conditionType)
    {
        // Arrange
        _model.AddParameters(new ParametersData
        {
            ConditionType = conditionType
        });
        var viewModel = _model.GetParametersViewModels().First() as IRemovable ?? throw new Exception();

        // Act
        viewModel.RemoveCommand.Execute(null);

        // Assert
        Assert.That(_model.ParametersContainer.Conditions.Count, Is.EqualTo(0));
    }
}