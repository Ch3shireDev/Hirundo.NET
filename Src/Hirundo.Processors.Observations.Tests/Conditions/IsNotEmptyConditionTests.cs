using Hirundo.Commons;
using Hirundo.Processors.Observations.Conditions;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.Tests.Conditions;

[TestFixture]
public class IsNotEmptyConditionTests
{
    [Test]
    public void GivenEmptyValue_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange
        var condition = new IsNotEmptyCondition("AGE");
        var observation = new Observation(["AGE"], new object[] { "" });

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
        var observation = new Observation(["AGE"], new object[] { "J" });

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