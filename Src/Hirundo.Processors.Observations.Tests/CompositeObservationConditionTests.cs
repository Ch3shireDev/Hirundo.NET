using Hirundo.Commons;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.Tests;

[TestFixture]
public class CompositeObservationConditionTests
{
    [SetUp]
    public void SetUp()
    {
        _filter1 = new Mock<IObservationCondition>();
        _filter2 = new Mock<IObservationCondition>();

        _compositeCondition = new CompositeObservationCondition(_filter1.Object, _filter2.Object);
    }

    private Mock<IObservationCondition> _filter1 = null!;
    private Mock<IObservationCondition> _filter2 = null!;
    private CompositeObservationCondition _compositeCondition = null!;

    [Test]
    public void GivenTwoTrueFilters_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        _filter1.Setup(f => f.IsAccepted(It.IsAny<Observation>())).Returns(true);
        _filter2.Setup(f => f.IsAccepted(It.IsAny<Observation>())).Returns(true);
        var observation = new Observation();

        // Act
        var result = _compositeCondition.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenOneFalseFilter_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange
        _filter1.Setup(f => f.IsAccepted(It.IsAny<Observation>())).Returns(true);
        _filter2.Setup(f => f.IsAccepted(It.IsAny<Observation>())).Returns(false);
        var observation = new Observation();

        // Act
        var result = _compositeCondition.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }
}