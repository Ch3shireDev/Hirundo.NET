using Hirundo.Commons;
using NUnit.Framework;
using System.Text;

namespace Hirundo.Processors.Observations.Tests;

[TestFixture]
public class IsEqualConditionTests
{
    [Test]
    public void GivenComplementaryValue_WhenIsAccepteded_ReturnsTrue()
    {
        // Arrange
        var valueName = "SEX";
        var value = "F";
        var filter = new IsEqualCondition(valueName, value);

        var observation = new Observation(["SEX"], ["F"]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenWrongValue_WhenIsAccepteded_ReturnsFalse()
    {
        // Arrange
        var valueName = "SPECIES";
        var value = "REG.REG";
        var filter = new IsEqualCondition(valueName, value);

        var observation = new Observation(["SPECIES"], ["REG.SCI"]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GivenValueWithDifferentReferenceButSameValue_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        var valueName = "SPECIES";
        var stringBuilder = new StringBuilder("REG.REG");
        var filter = new IsEqualCondition(valueName, stringBuilder.ToString());

        var observation = new Observation(["SPECIES", "XYZ"], ["REG.REG", 123]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenValueOfDifferentTypeButSameValue_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        var valueName = "WEIGHT";
        var filter = new IsEqualCondition(valueName, "10.0");

        var observation = new Observation(["WEIGHT"], [10.0M]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.True);
    }


    [Test]
    [TestCase("F", "M", false)]
    [TestCase("F", "F", true)]
    [TestCase("F", null, false)]
    [TestCase(null, "F", false)]
    [TestCase(null, null, true)]
    [TestCase(null, "", true)]
    [TestCase("", null, true)]
    [TestCase("", "", true)]
    [TestCase("", "F", false)]
    [TestCase("F", "", false)]
    [TestCase(100, 100, true)]
    [TestCase(100, 200, false)]
    [TestCase(100, null, false)]
    [TestCase(null, 100, false)]
    [TestCase("100", 100, true)]
    [TestCase("100", 100.0, true)]
    [TestCase(100, "100.0", true)]
    [TestCase(100, "200.0", false)]
    [TestCase("200", 300.0, false)]
    public void GivenValuesToCompare_WhenIsAccepted_ReturnsIsEqual(object? conditionValue, object? observationValue, bool result)
    {
        // Arrange
        var valueName = "VALUE";
        var condition = new IsEqualCondition(valueName, conditionValue);

        var observation = new Observation([valueName], [observationValue]);

        // Act
        var isAccepted = condition.IsAccepted(observation);

        // Assert
        Assert.That(isAccepted, Is.EqualTo(result), message: $"Compare {conditionValue} ({conditionValue?.GetType()?.Name}) to {observationValue} ({observationValue?.GetType()?.Name}))");
    }
}