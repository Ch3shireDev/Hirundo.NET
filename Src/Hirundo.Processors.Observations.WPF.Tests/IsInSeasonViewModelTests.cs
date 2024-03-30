using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Processors.Observations.WPF.IsInSeason;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.WPF.Tests;

[TestFixture]
public class IsInSeasonViewModelTests
{
    [SetUp]
    public void Initialize()
    {
        _repository = new Mock<IDataLabelRepository>();
        _condition = new IsInSeasonCondition();
        _model = new IsInSeasonModel(_condition, _repository.Object);
        _viewModel = new IsInSeasonViewModel(_model);
    }

    private Mock<IDataLabelRepository> _repository = null!;
    private IsInSeasonCondition _condition = null!;
    private IsInSeasonModel _model = null!;
    private IsInSeasonViewModel _viewModel = null!;

    [Test]
    public void GivenTextDataType_AfterSetValueInViewModel_ConditionHasSetValue()
    {
        // Arrange
        _condition.DateColumnName = string.Empty;
        _condition.Season = new Season();

        // Act
        _viewModel.ValueName = "DATE";
        _viewModel.StartMonth = 7;
        _viewModel.StartDay = 15;
        _viewModel.EndMonth = 8;
        _viewModel.EndDay = 20;

        // Assert
        Assert.That(_condition.DateColumnName, Is.EqualTo("DATE"));
        Assert.That(_condition.Season.StartMonth, Is.EqualTo(7));
        Assert.That(_condition.Season.StartDay, Is.EqualTo(15));
        Assert.That(_condition.Season.EndMonth, Is.EqualTo(8));
        Assert.That(_condition.Season.EndDay, Is.EqualTo(20));
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