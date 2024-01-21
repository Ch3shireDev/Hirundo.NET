using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using NUnit.Framework;

namespace Hirundo.Repositories.DataLabels.Tests;

[TestFixture]
public class DataLabelRepositoryTests
{
    [SetUp]
    public void Setup()
    {
        _repository = new DataLabelRepository();
    }

    private DataLabelRepository _repository = null!;

    [Test]
    public void GivenEmptyRepository_WhenGetLabels_ReturnsEmpty()
    {
        // Arrange
        _repository.Clear();

        // Act
        var labels = _repository.GetLabels();

        // Assert
        Assert.That(labels, Is.Empty);
    }

    [Test]
    public void GivenRepositoryWithLabels_WhenGetLabels_ReturnsLabels()
    {
        // Arrange
        _repository.Clear();
        _repository.AddLabel(new DataLabel("label1"));
        _repository.AddLabel(new DataLabel("label2"));

        // Act
        var labels = _repository.GetLabels().ToArray();

        // Assert
        Assert.That(labels, Is.Not.Empty);
        Assert.That(labels, Has.Length.EqualTo(2));
        Assert.That(labels.First().Name, Is.EqualTo("label1"));
        Assert.That(labels.Last().Name, Is.EqualTo("label2"));
    }

    [Test]
    public void GivenNonEmptyRepository_WhenClear_ReturnsEmpty()
    {
        // Arrange
        _repository.Clear();
        _repository.AddLabel(new DataLabel("label1"));
        _repository.AddLabel(new DataLabel("label2"));

        // Act
        _repository.Clear();
        var labels = _repository.GetLabels();

        // Assert
        Assert.That(labels, Is.Empty);
    }

    [Test]
    public void GivenEmptyRepository_WhenAddLabels_ReturnsLabels()
    {
        // Arrange
        _repository.Clear();

        // Act
        _repository.AddLabels([new DataLabel("label3"), new DataLabel("label4")]);
        var labels = _repository.GetLabels().ToArray();

        // Assert
        Assert.That(labels, Is.Not.Empty);
        Assert.That(labels, Has.Length.EqualTo(2));
        Assert.That(labels.First().Name, Is.EqualTo("label3"));
        Assert.That(labels.Last().Name, Is.EqualTo("label4"));
    }

    [Test]
    public void GivenSubscribedEvent_WhenAddLabels_EventListenersAreNotified()
    {
        // Arrange
        var eventRaised = false;
        _repository.LabelsChanged += (sender, args) => eventRaised = true;

        // Act
        _repository.AddLabels([new DataLabel("label3"), new DataLabel("label4")]);

        // Assert
        Assert.That(eventRaised, Is.True);
    }

    [Test]
    public void GivenSubscribedEvent_WhenClear_EventListenersAreNotified()
    {
        // Arrange
        var eventRaised = false;
        _repository.LabelsChanged += (sender, args) => eventRaised = true;

        // Act
        _repository.Clear();

        // Assert
        Assert.That(eventRaised, Is.True);
    }

    [Test]
    public void GivenSubscribedEvent_WhenAddLabel_EventListenersAreNotified()
    {
        // Arrange
        var eventRaised = false;
        _repository.LabelsChanged += (sender, args) => eventRaised = true;

        // Act
        _repository.AddLabel(new DataLabel("label3"));

        // Assert
        Assert.That(eventRaised, Is.True);
    }

    [Test]
    public void GivenExistingLabels_WhenUpdateLabels_SetsNewLabels()
    {
        // Arrange
        _repository.Clear();
        _repository.AddLabel(new DataLabel("label1", DataType.Numeric));
        _repository.AddLabel(new DataLabel("label2", DataType.Integer));

        // Act
        _repository.UpdateLabels([new DataLabel("label3"), new DataLabel("label4")]);

        // Assert
        var labels = _repository.GetLabels().ToArray();
        Assert.That(labels, Is.Not.Empty);
        Assert.That(labels, Has.Length.EqualTo(2));
        Assert.That(labels.First().Name, Is.EqualTo("label3"));
        Assert.That(labels.Last().Name, Is.EqualTo("label4"));
    }
}