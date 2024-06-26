﻿using Hirundo.Commons.Repositories;
using Hirundo.Processors.Statistics.Operations;
using Hirundo.Processors.WPF.Statistics.Histogram;
using Moq;

namespace Hirundo.Processors.WPF.Tests.Statistics;

[TestFixture]
public class HistogramViewModelTests
{
    [SetUp]
    public void Initialize()
    {
        _repository = new Mock<ILabelsRepository>();
        _parameters = new HistogramOperation();
        var speciesRepository = new Mock<ISpeciesRepository>();
        _model = new HistogramModel(_parameters, _repository.Object, speciesRepository.Object);
        _viewModel = new HistogramViewModel(_model);
    }

    private HistogramOperation _parameters = null!;
    private HistogramModel _model = null!;
    private HistogramViewModel _viewModel = null!;
    private Mock<ILabelsRepository> _repository = null!;

    [Test]
    public void GivenEmptyParameters_WhenSetValuesInViewModel_ThenParametersAreSet()
    {
        // Arrange
        _parameters.ValueName = string.Empty;
        _parameters.ResultPrefix = string.Empty;

        // Act
        _viewModel.ValueName = "D2";
        _viewModel.ResultPrefix = "D2-HISTOGRAM";
        _viewModel.Interval = 2;
        _viewModel.MinValue = 0;
        _viewModel.MaxValue = 10;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(_parameters.ValueName, Is.EqualTo("D2"));
            Assert.That(_parameters.ResultPrefix, Is.EqualTo("D2-HISTOGRAM"));
            Assert.That(_parameters.Interval, Is.EqualTo(2));
            Assert.That(_parameters.MinValue, Is.EqualTo(0));
            Assert.That(_parameters.MaxValue, Is.EqualTo(10));
        });
    }

    [Test]
    public void GivenSubscribedToRemoveEvent_WhenRemove_ThenListenerIsCalled()
    {
        // Arrange
        var listener = new object();

        _viewModel.Removed += (sender, args) =>
        {
            if (sender == _viewModel) listener = args.Parameters;
        };

        // Act
        _viewModel.RemoveCommand.Execute(null);

        // Assert
        Assert.That(listener, Is.EqualTo(_parameters));
    }
}