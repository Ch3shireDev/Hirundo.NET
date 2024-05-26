using Hirundo.Commons.Repositories;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.WPF.Returning.Alternative;
using Hirundo.Processors.WPF.Returning.CompareValues;
using Moq;

namespace Hirundo.Processors.WPF.Tests.Returning;

[TestFixture]
public class AlternativeViewModelTests
{
    [SetUp]
    public void Initialize()
    {
        _condition = new AlternativeReturningCondition();
        _repository = new Mock<ILabelsRepository>();
        var speciesRepository = new Mock<ISpeciesRepository>();
        _model = new AlternativeModel(_condition, _repository.Object, speciesRepository.Object);
        _viewModel = new AlternativeViewModel(_model);
    }

    private AlternativeReturningCondition _condition = null!;
    private AlternativeModel _model = null!;
    private AlternativeViewModel _viewModel = null!;
    private Mock<ILabelsRepository> _repository = null!;

    [Test]
    public void GivenSetConditions_WhenGetValuesFromViewModel_ThenValuesAreSet()
    {
        // Arrange
        _condition.Conditions.Clear();
        _condition.Conditions.Add(new IsEqualReturningCondition("DATA1", 123));
        _condition.Conditions.Add(new IsNotEqualReturningCondition("DATA2", 456));

        // Act
        var firstViewModel = _viewModel.FirstViewModel as IsEqualReturningViewModel;
        var secondViewModel = _viewModel.SecondViewModel as IsNotEqualReturningViewModel;

        // Assert
        Assert.That(firstViewModel, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(firstViewModel!.ValueName, Is.EqualTo("DATA1"));
            Assert.That(firstViewModel!.Value, Is.EqualTo("123"));

            Assert.That(secondViewModel, Is.Not.Null);
        });
        Assert.Multiple(() =>
        {
            Assert.That(secondViewModel!.ValueName, Is.EqualTo("DATA2"));
            Assert.That(secondViewModel!.Value, Is.EqualTo("456"));
        });
    }

    [Test]
    public void GivenSetConditions_WhenSetValuesToViewModel_ThenValuesAreSet()
    {
        // Arrange
        _condition.Conditions.Clear();
        _condition.Conditions.Add(new IsEqualReturningCondition("DATA1", 123));
        _condition.Conditions.Add(new IsNotEqualReturningCondition("DATA2", 456));

        // Act
        var firstViewModel = _viewModel.FirstViewModel as IsEqualReturningViewModel;
        var secondViewModel = _viewModel.SecondViewModel as IsNotEqualReturningViewModel;

        firstViewModel!.Value = "321";
        secondViewModel!.Value = "654";

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(firstViewModel.Value, Is.EqualTo("321"));
            Assert.That(secondViewModel.Value, Is.EqualTo("654"));
        });
    }

    [Test]
    public void GivenSetConditions_WhenSetValuesToViewModel_ThenModelValuesAreSet()
    {
        // Arrange
        var firstCondition = new IsEqualReturningCondition("DATA1", 123);
        var secondCondition = new IsNotEqualReturningCondition("DATA2", 456);

        _condition.Conditions.Clear();
        _condition.Conditions.Add(firstCondition);
        _condition.Conditions.Add(secondCondition);

        // Act
        var firstViewModel = _viewModel.FirstViewModel as IsEqualReturningViewModel;
        var secondViewModel = _viewModel.SecondViewModel as IsNotEqualReturningViewModel;

        firstViewModel!.Value = "321";
        secondViewModel!.Value = "654";

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(firstCondition.Value, Is.EqualTo("321"));
            Assert.That(secondCondition.Value, Is.EqualTo("654"));
        });
    }

    [Test]
    public void GivenConditions_WhenChangeSelectedCondition_ThenConditionsAreChanged()
    {
        // Arrange
        _condition.Conditions.Clear();
        _condition.Conditions.Add(new IsEqualReturningCondition("DATA1", 123));
        _condition.Conditions.Add(new IsNotEqualReturningCondition("DATA2", 456));

        var isGreaterParameter = _viewModel.Options.First(o => o.ConditionType == typeof(IsGreaterThanReturningCondition));
        var isLowerParameter = _viewModel.Options.First(o => o.ConditionType == typeof(IsLowerThanReturningCondition));

        // Act
        _viewModel.FirstParameter = isGreaterParameter;
        _viewModel.SecondParameter = isLowerParameter;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(_condition.Conditions[0], Is.InstanceOf<IsGreaterThanReturningCondition>());
            Assert.That(_condition.Conditions[1], Is.InstanceOf<IsLowerThanReturningCondition>());
        });
    }

    [Test]
    public void GivenConditions_WhenChangeSelectedCondition_ThenViewModelsAreChanged()
    {
        // Arrange
        _condition.Conditions.Clear();
        _condition.Conditions.Add(new IsEqualReturningCondition("DATA1", 123));
        _condition.Conditions.Add(new IsNotEqualReturningCondition("DATA2", 456));

        var isGreaterParameter = _viewModel.Options.First(o => o.ConditionType == typeof(IsGreaterThanReturningCondition));
        var isLowerParameter = _viewModel.Options.First(o => o.ConditionType == typeof(IsLowerThanReturningCondition));

        // Act
        _viewModel.FirstParameter = isGreaterParameter;
        _viewModel.SecondParameter = isLowerParameter;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(_viewModel.FirstViewModel, Is.InstanceOf<IsGreaterReturningViewModel>());
            Assert.That(_viewModel.SecondViewModel, Is.InstanceOf<IsLowerReturningViewModel>());
        });
    }

    [Test]
    public void GivenSetConditions_WhenChangeSecondConditionToNull_ThenThereIsOnlyOneCondition()
    {
        // Arrange
        _condition.Conditions.Clear();
        _condition.Conditions.Add(new IsEqualReturningCondition("DATA1", 123));
        _condition.Conditions.Add(new IsNotEqualReturningCondition("DATA2", 456));

        // Act
        _viewModel.SecondParameter = null;

        // Assert
        Assert.That(_condition.Conditions, Has.Count.EqualTo(1));
    }

    [Test]
    public void GivenSetConditions_WhenChangeFirstConditionToNull_SecondConditionBecomesFirstCondition()
    {
        // Arrange
        _condition.Conditions.Clear();
        _condition.Conditions.Add(new IsEqualReturningCondition("DATA1", 123));
        _condition.Conditions.Add(new IsNotEqualReturningCondition("DATA2", 456));

        // Act
        _viewModel.FirstParameter = null;

        // Assert
        Assert.That(_condition.Conditions, Has.Count.EqualTo(1));
        Assert.That(_condition.Conditions[0], Is.InstanceOf<IsNotEqualReturningCondition>());
        var secondCondition = _condition.Conditions[0] as IsNotEqualReturningCondition;
        Assert.Multiple(() =>
        {
            Assert.That(secondCondition!.ValueName, Is.EqualTo("DATA2"));
            Assert.That(secondCondition.Value, Is.EqualTo(456));
        });
    }
}