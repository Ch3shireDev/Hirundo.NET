using Hirundo.Commons.Models;
using Hirundo.Processors.Observations;

namespace Hirundo.Processors.Tests.Observations;

[TestFixture]
public class IsNotEmptyConditionTests
{
    [Test]
    public void GivenEmptyValue_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange
        var condition = new IsNotEmptyCondition("AGE");
        var observation = new Observation(["AGE"], [""]);

        // Act
        var result = condition.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GivenNonEmptyValue_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        var condition = new IsNotEmptyCondition("AGE");
        var observation = new Observation(["AGE"], ["J"]);

        // Act
        var result = condition.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenNullValue_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange
        var condition = new IsNotEmptyCondition("AGE");
        var observation = new Observation(["AGE"], [null]);

        // Act
        var result = condition.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }
}