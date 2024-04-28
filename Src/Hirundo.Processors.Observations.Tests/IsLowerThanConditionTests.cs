using Hirundo.Commons.Models;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.Tests;

[TestFixture]
public class IsLowerThanConditionTests
{
    [Test]
    public void GivenComplementaryValue_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        var valueName = "AGE";
        var value = 18;
        var filter = new IsLowerThanCondition(valueName, value);

        var observation = new Observation(["AGE"], [17]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    [TestCase(18, 18, false)]
    [TestCase(17, 18, true)]
    [TestCase("17", 18, true)]
    [TestCase(19, 18, false)]
    [TestCase("19", 18, false)]
    [TestCase(null, 18, false)]
    [TestCase("", 18, false)]
    [TestCase("abc", 18, false)]
    [TestCase("2020-01-01", "2020-01-02", true)]
    public void GivenTwoValues_WhenIsAccepted_ReturnsIfIsLower(object? lowerValue, object greaterValue, bool expectedResult)
    {
        // Arrange
        var valueName = "DATA";
        var filter = new IsLowerThanCondition(valueName, greaterValue);
        var observation = new Observation(["DATA"], [lowerValue]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}