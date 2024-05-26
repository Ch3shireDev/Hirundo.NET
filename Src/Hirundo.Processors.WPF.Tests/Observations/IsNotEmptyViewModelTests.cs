using Hirundo.Commons.Repositories;
using Hirundo.Processors.WPF.Observations.IsNotEmpty;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.WPF.Tests;

[TestFixture]
public class IsNotEmptyViewModelTests
{
    [SetUp]
    public void Setup()
    {
        _repository = new Mock<ILabelsRepository>();

        _condition = new IsNotEmptyCondition();
        var speciesRepository = new Mock<ISpeciesRepository>();
        _model = new IsNotEmptyModel(_condition, _repository.Object, speciesRepository.Object);
        _viewModel = new IsNotEmptyViewModel(_model);
    }

    private Mock<ILabelsRepository> _repository = null!;
    private IsNotEmptyCondition _condition = null!;
    private IsNotEmptyModel _model = null!;
    private IsNotEmptyViewModel _viewModel = null!;

    [Test]
    public void GivenTextDataType_AfterSetValueInViewModel_ConditionHasSetValue()
    {
        // Arrange
        _viewModel.ValueName = "";

        // Act
        _viewModel.ValueName = "AGE";

        // Assert
        Assert.That(_condition.ValueName, Is.EqualTo("AGE"));
    }

    [Test]
    public void GivenSubscribedToRemovedEvent_WhenRunRemoveCommand_ThenEventIsRaised()
    {
        // Arrange
        var eventRaised = false;
        _viewModel.Removed += (sender, args) => eventRaised = true;

        // Act
        _viewModel.RemoveCommand.Execute(null);

        // Assert
        Assert.That(eventRaised, Is.True);
    }
}