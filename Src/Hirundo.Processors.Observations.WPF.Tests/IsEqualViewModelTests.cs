using Hirundo.Commons;
using Hirundo.Processors.Observations.Conditions;
using Hirundo.Processors.Observations.WPF.IsEqual;
using Hirundo.Repositories.DataLabels;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.WPF.Tests;

[TestFixture]
public class IsEqualViewModelTests
{
    [SetUp]
    public void Setup()
    {
        _repository = new Mock<IDataLabelRepository>();

        _condition = new IsEqualCondition();
        _model = new IsEqualModel(_condition, _repository.Object);
        _viewModel = new IsEqualViewModel(_model);
    }

    private Mock<IDataLabelRepository> _repository = null!;

    private IsEqualCondition _condition = null!;
    private IsEqualModel _model = null!;
    private IsEqualViewModel _viewModel = null!;

    [Test]
    public void GivenDataLabelsInRepository_WhenGetLabels_ViewModelShouldShowDataLabels()
    {
        // Arrange
        var dataLabels = new List<DataLabel>
        {
            new("label1"),
            new("label2"),
            new("label3")
        };
        _repository.Setup(r => r.GetLabels()).Returns(dataLabels);

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
    public void GivenSelectedDataLabel_WhenGetSelectedLabel_ViewModelShouldShowSelectedDataLabel()
    {
        // Arrange
        var dataLabels = new List<DataLabel>
        {
            new("label1"),
            new("label2"),
            new("label3")
        };
        _repository.Setup(r => r.GetLabels()).Returns(dataLabels);

        // Act
        _viewModel.SelectedLabel = dataLabels[1];

        // Assert
        Assert.That(_model.SelectedLabel, Is.EqualTo(dataLabels[1]));
        Assert.That(_condition.ValueName, Is.EqualTo("label2"));
    }
}