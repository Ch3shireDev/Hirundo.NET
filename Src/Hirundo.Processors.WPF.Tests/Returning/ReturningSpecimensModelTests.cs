using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.WPF.Returning;
using Hirundo.Processors.WPF.Returning.Alternative;
using Hirundo.Processors.WPF.Returning.CompareValues;
using Moq;

namespace Hirundo.Processors.WPF.Tests.Returning;

[TestFixture]
public class ReturningSpecimensModelTests
{
    [Test]
    public void GivenAlternativeViewModelSelected_WhenAdded_IsNotInfiniteLoop()
    {
        // Arrange
        var repository = new Mock<ILabelsRepository>();
        var speciesRepository = new Mock<ISpeciesRepository>();
        var model = new ReturningSpecimensModel(repository.Object, speciesRepository.Object);
        var viewModel = new ParametersBrowserViewModel(model);

        // Act
        viewModel.SelectedParameter = viewModel.Options.FirstOrDefault(p => p.ConditionType == typeof(AlternativeReturningCondition));
        viewModel.AddParameters();

        // Assert
        Assert.That(viewModel.ParametersViewModels, Has.Count.EqualTo(1));
        var alternativeViewModel = viewModel.ParametersViewModels[0] as AlternativeViewModel;
        Assert.That(alternativeViewModel, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(alternativeViewModel!.FirstViewModel, Is.Not.InstanceOf<AlternativeViewModel>());
            Assert.That(alternativeViewModel!.SecondViewModel, Is.Not.InstanceOf<AlternativeViewModel>());
        });
    }

    [Test]
    public void GivenAlternativeViewModel_WhenLoaded_RemembersLastConfig()
    {
        // Arrange
        var config = new AlternativeReturningCondition
        {
            Conditions = [
                new IsEqualReturningCondition{
                    ValueName = "abc1",
                    Value = "def1"
                },
                new IsEqualReturningCondition{
                    ValueName = "abc2",
                    Value = "def2"
                },
            ]
        };

        // Act
        var model = new AlternativeModel(config, new Mock<ILabelsRepository>().Object, new Mock<ISpeciesRepository>().Object);
        var viewModel = new AlternativeViewModel(model);

        // Assert
        Assert.That(viewModel.FirstViewModel, Is.Not.Null);
        Assert.That(viewModel.SecondViewModel, Is.Not.Null);

        Assert.That(viewModel.FirstViewModel, Is.InstanceOf<IsEqualReturningViewModel>());
        Assert.That(viewModel.SecondViewModel, Is.InstanceOf<IsEqualReturningViewModel>());

        var firstViewModel = viewModel.FirstViewModel as IsEqualReturningViewModel;
        var secondViewModel = viewModel.SecondViewModel as IsEqualReturningViewModel;

        Assert.That(firstViewModel!.ValueName, Is.EqualTo("abc1"));
        Assert.That(firstViewModel.Value, Is.EqualTo("def1"));

        Assert.That(secondViewModel!.ValueName, Is.EqualTo("abc2"));
        Assert.That(secondViewModel.Value, Is.EqualTo("def2"));
    }
}