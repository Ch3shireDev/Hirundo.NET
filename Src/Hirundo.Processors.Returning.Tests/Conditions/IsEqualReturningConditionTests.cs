using Hirundo.Commons;
using Hirundo.Processors.Returning.Conditions;
using NUnit.Framework;

namespace Hirundo.Processors.Returning.Tests.Conditions;

[TestFixture]
public class IsEqualReturningConditionTests
{
    [Test]
    public void GivenSpecimenWithCorrectValue_WhenIsReturning_ReturnsTrue()
    {
        // Arrange
        var filter = new IsEqualReturningCondition("IS_RETURNING", true);

        var specimen = new Specimen("AB123", [
            new Observation(["IS_RETURNING"], [true])
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenSpecimenWithIncorrectValue_WhenIsReturning_ReturnsFalse()
    {
        // Arrange
        var filter = new IsEqualReturningCondition("IS_RETURNING", true);

        var specimen = new Specimen("AB123", [
            new Observation(["IS_RETURNING"], [false])
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GivenNullValue_WhenIsReturning_ReturnsFalse()
    {
        // Arrange
        var filter = new IsEqualReturningCondition("IS_EMPTY", null);

        var specimen = new Specimen("AB123", [
            new Observation(["IS_EMPTY"], [null])
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.True);
    }
}