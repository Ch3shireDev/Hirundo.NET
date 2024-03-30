using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
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
        _repository.AddLabel(new DataLabel("label2", DataType.Number));

        // Act
        _repository.SetLabels([new DataLabel("label3"), new DataLabel("label4")]);

        // Assert
        var labels = _repository.GetLabels().ToArray();
        Assert.That(labels, Is.Not.Empty);
        Assert.That(labels, Has.Length.EqualTo(2));
        Assert.That(labels.First().Name, Is.EqualTo("label3"));
        Assert.That(labels.Last().Name, Is.EqualTo("label4"));
    }

    [Test]
    public void GivenAdditionalLabels_WhenGetLabels_ReturnsAllLabels()
    {
        // Arrange
        _repository.Clear();
        _repository.AddLabel(new DataLabel("label1", DataType.Numeric));
        _repository.AddAdditionalLabel(new DataLabel("label2", DataType.Number));

        // Act
        var labels = _repository.GetLabels().ToArray();

        // Assert
        Assert.That(labels, Is.Not.Empty);
        Assert.That(labels, Has.Length.EqualTo(2));
    }

    [Test]
    public void GivenSubscribedEvent_WhenAddAdditionalLabel_EventListenersAreNotified()
    {
        // Arrange
        var eventRaised = false;
        _repository.LabelsChanged += (sender, args) => eventRaised = true;

        // Act
        _repository.AddAdditionalLabel(new DataLabel("label3"));

        // Assert
        Assert.That(eventRaised, Is.True);
    }

    [Test]
    public void GivenExistingLabelsAndAdditionalLabels_WhenUpdateLabels_AdditionalLabelsAreNotAffected()
    {
        // Arrange
        _repository.Clear();
        _repository.AddLabel(new DataLabel("label1", DataType.Numeric));
        _repository.AddAdditionalLabel(new DataLabel("additional-label", DataType.Number));

        // Act
        _repository.SetLabels([new DataLabel("label3"), new DataLabel("label4")]);

        // Assert
        var labels = _repository.GetLabels().ToArray();
        Assert.That(labels, Is.Not.Empty);
        Assert.That(labels, Has.Length.EqualTo(3));
        Assert.That(labels[0].Name, Is.EqualTo("label3"));
        Assert.That(labels[1].Name, Is.EqualTo("label4"));
        Assert.That(labels[2].Name, Is.EqualTo("additional-label"));
    }

    [Test]
    public void GivenExistingAdditionalLabel_WhenRemoveAdditionalLabelWithSameName_AdditionalLabelIsRemoved()
    {
        // Arrange
        _repository.Clear();
        _repository.AddAdditionalLabel(new DataLabel("additional-label-1", DataType.Number));
        _repository.AddAdditionalLabel(new DataLabel("additional-label-2", DataType.Number));

        // Act
        _repository.RemoveAdditionalLabel(new DataLabel("additional-label-1"));

        // Assert
        var labels = _repository.GetLabels().ToArray();
        Assert.That(labels, Has.Length.EqualTo(1));
    }

    [Test]
    public void GivenSubscribedEventListener_WhenRemoveAdditionalLabel_EventListenersAreNotified()
    {
        // Arrange
        var eventRaised = false;
        _repository.AddAdditionalLabel(new DataLabel("additional-label-1"));
        _repository.LabelsChanged += (_, _) => eventRaised = true;

        // Act
        _repository.RemoveAdditionalLabel(new DataLabel("additional-label-1"));

        // Assert
        Assert.That(eventRaised, Is.True);
    }

    [Test]
    public void GivenRepositoryWithValue_WhenAddValueSecondTime_GetsOnlyOneLine()
    {
        // Arrange
        _repository.Clear();
        _repository.AddAdditionalLabel(new DataLabel("label1", DataType.Numeric));

        // Act
        _repository.AddAdditionalLabel(new DataLabel("label1", DataType.Numeric));

        // Assert
        var labels = _repository.GetLabels().ToArray();
        Assert.That(labels, Has.Length.EqualTo(1));
    }
}