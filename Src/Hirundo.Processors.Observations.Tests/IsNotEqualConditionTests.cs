using Hirundo.Commons.Models;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.Tests;

[TestFixture]
public class IsNotEqualConditionTests
{
    [Test]
    [TestCase("F", "M", true)]
    [TestCase("F", "F", false)]
    [TestCase("F", null, true)]
    [TestCase(null, "F", true)]
    [TestCase(null, null, false)]
    [TestCase(null, "", false)]
    [TestCase("", null, false)]
    [TestCase("", "", false)]
    [TestCase("", "F", true)]
    [TestCase("F", "", true)]
    [TestCase(100, 100, false)]
    [TestCase(100, 200, true)]
    [TestCase(100, null, true)]
    [TestCase(null, 100, true)]
    [TestCase("100", 100, false)]
    [TestCase("100", 100.0, false)]
    [TestCase(100, "100.0", false)]
    [TestCase(100, "200.0", true)]
    [TestCase("200", 300.0, true, Description = "Compare string 200 to decimal 300")]
    public void GivenValuesToCompare_WhenIsAccepted_ReturnsIsNotEqual(object? conditionValue, object? observationValue, bool result)
    {
        // Arrange
        var valueName = "VALUE";
        var condition = new IsNotEqualCondition(valueName, conditionValue);

        var observation = new Observation([valueName], [observationValue]);

        // Act
        var isAccepted = condition.IsAccepted(observation);

        // Assert
        Assert.That(isAccepted, Is.EqualTo(result), $"Compare {conditionValue} ({conditionValue?.GetType()?.Name}) to {observationValue} ({observationValue?.GetType()?.Name}))");
    }


    [Test]
    public void GivenOtherValue_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        var valueName = "SEX";
        var value = "F";

        var filter = new IsNotEqualCondition(valueName, value);

        var observation = new Observation(["SEX"], ["M"]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenValue_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange
        var valueName = "SPECIES";
        var value = "REG.REG";

        var filter = new IsNotEqualCondition(valueName, value);

        var observation = new Observation(["SPECIES"], ["REG.REG"]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GivenNullValue_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        var valueName = "SPECIES";
        var value = "REG.REG";

        var filter = new IsNotEqualCondition(valueName, value);

        var observation = new Observation(["SPECIES"], [null]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenEmptyConditionValue_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        var valueName = "SPECIES";
        var value = "";

        var filter = new IsNotEqualCondition(valueName, value);

        var observation = new Observation(["SPECIES"], ["REG.REG"]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenNullConditionValue_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        var valueName = "SPECIES";
        object? value = null;

        var filter = new IsNotEqualCondition(valueName, value);

        var observation = new Observation(["SPECIES"], ["REG.REG"]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenSameValueAsString_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange
        var valueName = "MASS";
        var value = "100";

        var filter = new IsNotEqualCondition(valueName, value);
        var observation = new Observation(["MASS"], [100.0M]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GivenNullValueAndEmptyString_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange
        var valueName = "SPECIES";
        var value = "";

        var filter = new IsNotEqualCondition(valueName, value);

        var observation = new Observation(["SPECIES"], [null]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GivenEmptyValueAndNull_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange
        var valueName = "SPECIES";
        object? value = null;

        var filter = new IsNotEqualCondition(valueName, value);

        var observation = new Observation(["SPECIES"], [""]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }
}