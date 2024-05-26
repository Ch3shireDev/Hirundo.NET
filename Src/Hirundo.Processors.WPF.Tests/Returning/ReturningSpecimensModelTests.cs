using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.WPF.Returning;
using Hirundo.Processors.WPF.Returning.Alternative;
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
}