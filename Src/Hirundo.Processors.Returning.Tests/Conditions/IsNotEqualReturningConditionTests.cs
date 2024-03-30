using Hirundo.Commons;
using Hirundo.Commons.Models;
using Hirundo.Processors.Returning.Conditions;
using NUnit.Framework;

namespace Hirundo.Processors.Returning.Tests.Conditions;

[TestFixture]
public class IsNotEqualReturningConditionTests
{
    [Test]
    public void GivenSpecimenWithCorrectValue_WhenIsReturning_ReturnsFalse()
    {
        // Arrange
        var filter = new IsNotEqualReturningCondition("SEX", "F");

        var specimen = new Specimen("AB123", [
            new Observation(["SEX"], ["M"])
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    [TestCase("F", "F", "F", false)]
    [TestCase("F", null, "F", false)]
    [TestCase("M", null, "F", true)]
    [TestCase("M", "F", "F", true)]
    public void GivenTwoObservationsWithValues_WhenIsReturning_ReturnsExpectedValue(object? observationValue1, object? observationValue2, object? value, bool expectedResult)
    {
        // Arrange
        var filter = new IsNotEqualReturningCondition("DATA", value);

        var specimen = new Specimen("AB123", [
            new Observation(["DATA"], [observationValue1]),
            new Observation(["DATA"], [observationValue2])
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
