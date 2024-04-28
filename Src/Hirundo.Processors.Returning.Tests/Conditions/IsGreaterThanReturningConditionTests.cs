using Hirundo.Commons.Models;
using Hirundo.Processors.Returning.Conditions;
using NUnit.Framework;

namespace Hirundo.Processors.Returning.Tests.Conditions;

[TestFixture]
public class IsGreaterThanReturningConditionTests
{
    [Test]
    public void GivenObservationGreaterThanValue_WhenIsGreaterThanReturningCondition_ThenReturnTrue()
    {
        // Arrange
        var condition = new IsGreaterThanReturningCondition("DATA", 5);

        var specimen = new Specimen("AB123", [
            new Observation(["DATA"], [6])
        ]);

        // Act
        var result = condition.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    [TestCase(5, 6, 5, true)]
    [TestCase(5, 5, 5, false)]
    [TestCase(6, null, 5, true)]
    [TestCase("2020-02-03", "2020-02-01", "2020-02-02", true)]
    [TestCase("2020-02-01", "2020-02-01", "2020-02-02", false)]
    public void GivenTwoObservationsForSpecimen_WhenIsReturning_ThenReturnExpectedResult(object? observationValue1, object? observationValue2, object? value, bool expectedResult)
    {
        // Arrange
        var condition = new IsGreaterThanReturningCondition("DATA", value);

        var specimen = new Specimen("AB123", [
            new Observation(["DATA"], [observationValue1]),
            new Observation(["DATA"], [observationValue2])
        ]);

        // Act
        var result = condition.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}