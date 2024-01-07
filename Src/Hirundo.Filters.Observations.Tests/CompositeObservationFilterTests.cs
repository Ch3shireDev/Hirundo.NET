using Hirundo.Commons;
using Moq;
using NUnit.Framework;

namespace Hirundo.Filters.Observations.Tests;

[TestFixture]
public class CompositeObservationFilterTests
{
    [SetUp]
    public void SetUp()
    {
        _filter1 = new Mock<IObservationFilter>();
        _filter2 = new Mock<IObservationFilter>();

        _compositeFilter = new CompositeObservationFilter(_filter1.Object, _filter2.Object);
    }

    private Mock<IObservationFilter> _filter1 = null!;
    private Mock<IObservationFilter> _filter2 = null!;
    private CompositeObservationFilter _compositeFilter = null!;

    [Test]
    public void GivenTwoTrueFilters_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        _filter1.Setup(f => f.IsAccepted(It.IsAny<Observation>())).Returns(true);
        _filter2.Setup(f => f.IsAccepted(It.IsAny<Observation>())).Returns(true);
        var observation = new Observation();

        // Act
        var result = _compositeFilter.IsAccepted(observation);

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
        var result = _compositeFilter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }
}