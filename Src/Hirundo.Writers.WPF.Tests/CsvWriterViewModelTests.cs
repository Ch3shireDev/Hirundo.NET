using Hirundo.Commons.Repositories;
using Hirundo.Writers.WPF.CsvWriter;
using Moq;
using NUnit.Framework;

namespace Hirundo.Writers.WPF.Tests;

[TestFixture]
public class CsvWriterViewModelTests
{
    [SetUp]
    public void SetUp()
    {
        _parameters = new CsvSummaryWriterParameters();
        var speciesRepository = new Mock<ISpeciesRepository>();
        var labelsRepository = new Mock<ILabelsRepository>();
        _model = new CsvWriterModel(_parameters, labelsRepository.Object, speciesRepository.Object);
        _viewModel = new CsvWriterViewModel(_model);
    }

    private CsvSummaryWriterParameters _parameters = null!;
    private CsvWriterModel _model = null!;
    private CsvWriterViewModel _viewModel = null!;

    [Test]
    public void GivenEmptyParameters_WhenSetPath_ParametersAreChanged()
    {
        // Arrange
        List<string?> raisedPropertyNames = [];
        _viewModel.PropertyChanged += (sender, args) => raisedPropertyNames.Add(args?.PropertyName);

        // Act
        _viewModel.Path = "test.csv";

        // Assert
        Assert.That(raisedPropertyNames.Contains(nameof(_viewModel.Path)), Is.True);
        Assert.That(_parameters.Path, Is.EqualTo("test.csv"));
    }
}