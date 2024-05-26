using Hirundo.Commons.Models;
using Hirundo.Processors.Observations;

namespace Hirundo.Processors.Tests.Observations;

[TestFixture]
public class IsInSetConditionTests
{
    [Test]
    public void GivenValueInSet_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        var condition = new IsInSetCondition("AGE", "I", "J");
        var observation = new Observation(["AGE"], ["I"]);

        // Act
        var result = condition.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenValueOutsideSet_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange
        var condition = new IsInSetCondition("AGE", "I", "J");
        var observation = new Observation(["AGE"], ["K"]);

        // Act
        var result = condition.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GivenColumnOutsideObservation_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange
        var condition = new IsInSetCondition("AGE", "I", "J");
        var observation = new Observation(["ID"], [1]);

        // Act
        var result = condition.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }
}