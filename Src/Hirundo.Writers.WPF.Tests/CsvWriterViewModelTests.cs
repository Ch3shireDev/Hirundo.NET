using Hirundo.Commons.Repositories;
using Hirundo.Writers.WPF.CsvWriter;
using Moq;
using NUnit.Framework;

namespace Hirundo.Writers.WPF.Tests;

[TestFixture]
public class CsvWriterViewModelTests
{
    private CsvSummaryWriterParameters _parameters = null!;
    private CsvWriterModel _model = null!;
    private CsvWriterViewModel _viewModel = null!;

    [SetUp]
    public void SetUp()
    {
        _parameters = new CsvSummaryWriterParameters();
        var speciesRepository = new Mock<ISpeciesRepository>();
        var labelsRepository = new Mock<ILabelsRepository>();
        _model = new CsvWriterModel(_parameters, labelsRepository.Object, speciesRepository.Object);
        _viewModel = new CsvWriterViewModel(_model);
    }

    [Test]
    public void GivenEmptyParameters_WhenSetPath_ParametersAreChanged()
    {
        string? raisedPropertyName = null;
        _viewModel.PropertyChanged += (sender, args) => raisedPropertyName = args.PropertyName;

        _viewModel.Path = "test.csv";

        Assert.That(raisedPropertyName, Is.EqualTo(nameof(_viewModel.Path)));
        Assert.That(_parameters.Path, Is.EqualTo("test.csv"));
    }
}
