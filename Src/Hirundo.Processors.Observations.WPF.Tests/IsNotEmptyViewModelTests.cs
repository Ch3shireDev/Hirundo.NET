using Hirundo.Commons.Repositories.Labels;
using Hirundo.Processors.Observations.WPF.IsNotEmpty;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.WPF.Tests;

[TestFixture]
public class IsNotEmptyViewModelTests
{
    [SetUp]
    public void Setup()
    {
        _repository = new Mock<IDataLabelRepository>();

        _condition = new IsNotEmptyCondition();
        _model = new IsNotEmptyModel(_condition, _repository.Object);
        _viewModel = new IsNotEmptyViewModel(_model);
    }

    private Mock<IDataLabelRepository> _repository = null!;
    private IsNotEmptyCondition _condition = null!;
    private IsNotEmptyModel _model = null!;
    private IsNotEmptyViewModel _viewModel = null!;

    [Test]
    public void GivenTextDataType_AfterSetValueInViewModel_ConditionHasSetValue()
    {
        // Arrange

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