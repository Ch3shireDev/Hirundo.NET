using Hirundo.Commons;
using Hirundo.Commons.Models;
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

    [Test]
    [TestCase(1, 1, 1, true)]
    [TestCase(1, null, 1, true)]
    [TestCase(null, null, 1, false)]
    [TestCase(1, 2, "1", true)]
    [TestCase(2, 2, "1", false)]
    [TestCase("1", "2", 1, true)]
    [TestCase("1.0", "2.0", 1, true)]
    [TestCase("2.0", "2.0", 1, false)]
    public void GivenNullValueAndMatchingValue_WhenIsReturning_ReturnsExpectedValue(object? observationValueA, object? observationValueB, object? value, bool expectedResult)
    {
        // Arrange
        var filter = new IsEqualReturningCondition("DATA", value);

        var specimen = new Specimen("AB123", [
            new Observation(["DATA"], [observationValueA]),
            new Observation(["DATA"], [observationValueB]),
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
