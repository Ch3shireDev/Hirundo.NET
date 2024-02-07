using Hirundo.Commons;
using Hirundo.Processors.Observations.Conditions;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.Tests.Conditions;

[TestFixture]
public class IsInSetConditionTests
{
    [Test]
    public void GivenValueInSet_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        var condition = new IsInSetCondition("AGE", "I", "J");
        var observation = new Observation(["AGE"], new object[] { "I" });

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
        var observation = new Observation(["AGE"], new object[] { "K" });

        // Act
        var result = condition.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GivenColumnOutsideObservation_WhenIsAccepted_ThrowsException()
    {
        // Arrange
        var condition = new IsInSetCondition("AGE", "I", "J");
        var observation = new Observation(["ID"], new object[] { 1 });

        // Act
        bool act() => condition.IsAccepted(observation);

        // Assert
        Assert.That(act, Throws.Exception);
        Assert.That(act, Throws.TypeOf<KeyNotFoundException>());
    }
}