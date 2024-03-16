using Hirundo.Commons;
using NUnit.Framework;
using System.Text;

namespace Hirundo.Processors.Observations.Tests;

[TestFixture]
public class IsEqualObservationConditionTests
{
    [Test]
    public void GivenComplementaryValue_WhenIsAccepteded_ReturnsTrue()
    {
        // Arrange
        var valueName = "SEX";
        var value = "F";
        var filter = new IsEqualObservationCondition(valueName, value);

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
        var filter = new IsEqualObservationCondition(valueName, value);

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
        var filter = new IsEqualObservationCondition(valueName, stringBuilder.ToString());

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
        var filter = new IsEqualObservationCondition(valueName, "10.0");

        var observation = new Observation(["WEIGHT"], [10.0M]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.True);
    }
}