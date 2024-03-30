using Hirundo.Commons.Repositories;
using Hirundo.Processors.Statistics.Operations;
using Hirundo.Processors.Statistics.WPF.Average;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Statistics.WPF.Tests;

[TestFixture]
public class AverageViewModelTests
{
    AverageOperation _operation = null!;
    AverageModel _model = null!;
    AverageViewModel _viewModel = null!;

    Mock<IDataLabelRepository> _repository = null!;

    [SetUp]
    public void Initialize()
    {
        _repository = new Mock<IDataLabelRepository>();
        _operation = new AverageOperation("VALUE", "VALUE_PREFIX");
        _model = new AverageModel(_operation, _repository.Object);
        _viewModel = new AverageViewModel(_model);
    }

    [Test]
    public void GivenValueName_WhenSet_ValueNameIsChanged()
    {
        // Arrange
        _operation.ValueName = "VALUE";
        _operation.ResultPrefix = "VALUE_PREFIX";

        // Act
        _viewModel.ValueName = "VALUE2";
        _viewModel.ResultPrefix = "PREFIX2";

        Assert.That(_operation.ValueName, Is.EqualTo("VALUE2"));
        Assert.That(_operation.ResultPrefix, Is.EqualTo("PREFIX2"));
    }

    [Test]
    public void GivenInitialValuesInOperation_WhenGetValues_ViewModelHasValues()
    {
        // Arrange

        // Act
        _operation.ValueName = "VALUE3";
        _operation.ResultPrefix = "PREFIX3";

        // Assert
        Assert.That(_viewModel.ValueName, Is.EqualTo("VALUE3"));
        Assert.That(_viewModel.ResultPrefix, Is.EqualTo("PREFIX3"));
    }

    [Test]
    public void GivenPrefixSameAsValueName_WhenChangeValue_PrefixIsChanged()
    {
        // Arrange
        _viewModel.ValueName = "VALUE";
        _viewModel.ResultPrefix = "VALUE";
        var isChanged = false;
        _viewModel.PropertyChanged += (sender, args) => { if (args.PropertyName == nameof(AverageViewModel.ResultPrefix)) isChanged = true; };

        // Act
        _viewModel.ValueName = "VALUE2";

        // Assert
        Assert.That(_viewModel.ResultPrefix, Is.EqualTo("VALUE2"));
        Assert.That(isChanged, Is.True);
    }

    [Test]
    public void GivenPrefixOtherThanValueName_WhenChangeValue_PrefixIsNotChanged()
    {
        // Arrange
        _viewModel.ValueName = "VALUE";
        _viewModel.ResultPrefix = "PREFIX";

        // Act
        _viewModel.ValueName = "VALUE2";

        // Assert
        Assert.That(_viewModel.ResultPrefix, Is.EqualTo("PREFIX"));
    }
}
