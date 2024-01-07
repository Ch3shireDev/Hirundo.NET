using Hirundo.Commons;
using NUnit.Framework;

namespace Hirundo.Filters.Observations.Tests;

[TestFixture]
public class IsInTimeBlockFilterTests
{
    [Test]
    public void GivenHourInTimeBlock_WhenIsSelect_ReturnsTrue()
    {
        // Arrange
        var timeBlock = new TimeBlock(06, 08);
        var filter = new IsInTimeBlockFilter("HOUR", timeBlock);
        var observation = new Observation(["HOUR"], [7]);

        // Act
        var result = filter.IsSelected(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenHourInTimeBlockThroughMidnight_WhenIsSelect_ReturnsTrue()
    {
        // Arrange
        var timeBlock = new TimeBlock(23, 2);
        var filter = new IsInTimeBlockFilter("HOUR", timeBlock);
        var observation = new Observation(["HOUR"], [1]);

        // Act
        var result = filter.IsSelected(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenHourOutsideTimeBlock_WhenIsSelect_ReturnsFalse()
    {
        // Arrange
        var timeBlock = new TimeBlock(12, 14);
        var filter = new IsInTimeBlockFilter("HOUR", timeBlock);
        var observation = new Observation(["HOUR"], [15]);

        // Act
        var result = filter.IsSelected(observation);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GivenHourOutsideTimeBlockThroughMidnight_WhenIsSelect_ReturnsFalse()
    {
        // Arrange
        var timeBlock = new TimeBlock(22, 5);
        var filter = new IsInTimeBlockFilter("HOUR", timeBlock);
        var observation = new Observation(["HOUR"], [6]);

        // Act
        var result = filter.IsSelected(observation);

        // Assert
        Assert.That(result, Is.False);
    }
}