using Hirundo.App.WPF.Components;
using Hirundo.Commons.WPF;
using Hirundo.Databases.WPF;
using NUnit.Framework;

namespace Hirundo.App.WPF.Tests;

[TestFixture]
public class MainViewModelTests
{
    [SetUp]
    public void Setup()
    {
        var hirundoApp = new HirundoApp();
        var model = new MainModel(hirundoApp);
        _viewModel = new MainViewModel(model);
    }

    private MainViewModel _viewModel = null!;

    [Test]
    public void GivenStartingViewModel_WhenShow_SelectedViewModelShouldBeDataSource()
    {
        // Arrange

        // Act
        var selected = _viewModel.SelectedViewModel;

        // Assert
        Assert.That(selected, Is.InstanceOf<ParametersBrowserViewModel>());
        var browserViewModel = selected as ParametersBrowserViewModel;
        Assert.That(browserViewModel?.Title, Is.EqualTo(new DataSourceModel().Title));
    }

    [Test]
    public void GivenStartingViewModel_WhenShow_PreviousCommandShouldBeDisabled()
    {
        // Arrange
        _viewModel.SelectedViewModel = _viewModel.ViewModels.First();

        // Act
        var enabled = _viewModel.PreviousCommand.CanExecute(null);

        // Assert
        Assert.That(enabled, Is.False);
    }

    [Test]
    public void GivenStartingViewModel_WhenShow_NextCommandShouldBeEnabled()
    {
        // Arrange
        _viewModel.SelectedViewModel = _viewModel.ViewModels.First();

        // Act
        var enabled = _viewModel.NextCommand.CanExecute(null);

        // Assert
        Assert.That(enabled, Is.True);
    }

    [Test]
    public void GivenEndingViewModel_WhenShow_NextCommandShouldBeDisabled()
    {
        // Arrange
        _viewModel.SelectedViewModel = _viewModel.ViewModels.Last();

        // Act
        var enabled = _viewModel.NextCommand.CanExecute(null);

        // Assert
        Assert.That(enabled, Is.False);
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    public void AfterPressingNext_WhenShow_SelectedViewModelShouldBeNext(int startingIndex)
    {
        // Arrange
        _viewModel.SelectedViewModel = _viewModel.ViewModels[startingIndex];

        // Act
        _viewModel.NextCommand.Execute(null);
        var selected = _viewModel.SelectedViewModel;

        // Assert
        Assert.That(selected, Is.EqualTo(_viewModel.ViewModels[startingIndex + 1]));
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    public void AfterPressingPrevious_WhenShow_SelectedViewModelShouldBePrevious(int startingIndex)
    {
        // Arrange
        _viewModel.SelectedViewModel = _viewModel.ViewModels[startingIndex];

        // Act
        _viewModel.PreviousCommand.Execute(null);
        var selected = _viewModel.SelectedViewModel;

        // Assert
        Assert.That(selected, Is.EqualTo(_viewModel.ViewModels[startingIndex - 1]));
    }
}