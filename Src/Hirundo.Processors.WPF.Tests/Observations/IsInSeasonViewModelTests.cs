using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Processors.Observations;
using Hirundo.Processors.WPF.Observations.IsInSeason;
using Moq;

namespace Hirundo.Processors.WPF.Tests.Observations;

[TestFixture]
public class IsInSeasonViewModelTests
{
    [SetUp]
    public void Initialize()
    {
        _repository = new Mock<ILabelsRepository>();
        _condition = new IsInSeasonCondition();
        var speciesRepository = new Mock<ISpeciesRepository>();
        _model = new IsInSeasonModel(_condition, _repository.Object, speciesRepository.Object);
        _viewModel = new IsInSeasonViewModel(_model);
    }

    private Mock<ILabelsRepository> _repository = null!;
    private IsInSeasonCondition _condition = null!;
    private IsInSeasonModel _model = null!;
    private IsInSeasonViewModel _viewModel = null!;

    [Test]
    public void GivenTextDataType_AfterSetValueInViewModel_ConditionHasSetValue()
    {
        // Arrange
        _condition.Season = new Season();

        // Act
        _viewModel.StartMonth = 7;
        _viewModel.StartDay = 15;
        _viewModel.EndMonth = 8;
        _viewModel.EndDay = 20;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(_condition.Season.StartMonth, Is.EqualTo(7));
            Assert.That(_condition.Season.StartDay, Is.EqualTo(15));
            Assert.That(_condition.Season.EndMonth, Is.EqualTo(8));
            Assert.That(_condition.Season.EndDay, Is.EqualTo(20));
        });
    }

    [Test]
    public void GivenSubscribedListener_OnRemoveCommand_ThenEventIsRaised()
    {
        // Arrange
        object? argument = null;
        _viewModel.Removed += (sender, args) => argument = args.Parameters;

        // Act
        _viewModel.RemoveCommand.Execute(null);

        // Assert
        Assert.That(argument, Is.EqualTo(_condition));
    }
}