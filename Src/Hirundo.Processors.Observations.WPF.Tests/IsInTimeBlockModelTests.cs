using Hirundo.Commons;
using Hirundo.Processors.Observations.Conditions;
using Hirundo.Processors.Observations.WPF.IsInTimeBlock;
using Hirundo.Repositories.DataLabels;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.WPF.Tests;

[TestFixture]
public class IsInTimeBlockModelTests
{
    Mock<IDataLabelRepository> _repository = null!;
    IsInTimeBlockCondition _condition = null!;
    IsInTimeBlockModel _model = null!;
    IsInTimeBlockViewModel _viewModel = null!;

    [SetUp]
    public void Setup()
    {
        _repository = new Mock<IDataLabelRepository>();

        _condition = new IsInTimeBlockCondition();
        _model = new IsInTimeBlockModel(_condition, _repository.Object);
        _viewModel = new IsInTimeBlockViewModel(_model);
    }

    [Test]
    public void GivenDataLabelsInRepository_WhenGetDataLabels_ReturnsDataLabels()
    {
        // Arrange
        _repository.Setup(r => r.GetLabels()).Returns(new List<DataLabel>
        {
            new("label1"),
            new("label2"),
            new("label3")
        });

        // Act
        var labels = _viewModel.Labels;

        // Assert
        Assert.That(labels, Is.Not.Null);
        Assert.That(labels.Count, Is.EqualTo(3));
        Assert.That(labels[0].Name, Is.EqualTo("label1"));
        Assert.That(labels[1].Name, Is.EqualTo("label2"));
        Assert.That(labels[2].Name, Is.EqualTo("label3"));
    }

    [Test]
    public void GivenDataLabelsInRepository_WhenSelectLabel_SelectedLabelShouldBeSet()
    {
        // Arrange
        _repository.Setup(r => r.GetLabels()).Returns(new List<DataLabel>
        {
            new("label1"),
            new("label2"),
            new("label3")
        });

        // Act
        _viewModel.SelectedLabel = _viewModel.Labels[1];

        // Assert
        Assert.That(_viewModel.SelectedLabel, Is.Not.Null);
        Assert.That(_viewModel.SelectedLabel?.Name, Is.EqualTo("label2"));
        Assert.That(_model.ValueName, Is.EqualTo("label2"));
        Assert.That(_condition.ValueName, Is.EqualTo("label2"));
    }

    [Test]
    public void GivenSelectedValueNameInCondition_WhenGetSelectedLabel_SelectedLabelShouldBeSet()
    {
        // Arrange
        _condition.ValueName = "label2";
        _repository.Setup(r => r.GetLabels()).Returns(new List<DataLabel>
        {
            new("label1"),
            new("label2"),
            new("label3")
        });

        // Act
        var selectedLabel = _viewModel.SelectedLabel;

        // Assert
        Assert.That(selectedLabel, Is.Not.Null);
        Assert.That(selectedLabel?.Name, Is.EqualTo("label2"));
    }
}