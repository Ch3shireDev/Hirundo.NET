using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.Returning.WPF.Alternative;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Returning.WPF.Tests;

[TestFixture]
public class ReturningSpecimensModelTests
{
    [Test]
    public void GivenAlternativeViewModelSelected_WhenAdded_IsNotInfiniteLoop()
    {
        // Arrange
        var repository = new Mock<IDataLabelRepository>();
        var model = new ReturningSpecimensModel(repository.Object);
        var viewModel = new ParametersBrowserViewModel(model);

        // Act
        viewModel.SelectedParameter = viewModel.Options.FirstOrDefault(p => p.ConditionType == typeof(AlternativeReturningCondition));
        viewModel.AddParameters();

        // Assert
        Assert.That(viewModel.ParametersViewModels.Count, Is.EqualTo(1));
        var alternativeViewModel = viewModel.ParametersViewModels[0] as AlternativeViewModel;
        Assert.That(alternativeViewModel, Is.Not.Null);
        Assert.That(alternativeViewModel!.FirstViewModel, Is.Not.InstanceOf<AlternativeViewModel>());
        Assert.That(alternativeViewModel!.SecondViewModel, Is.Not.InstanceOf<AlternativeViewModel>());
    }
}