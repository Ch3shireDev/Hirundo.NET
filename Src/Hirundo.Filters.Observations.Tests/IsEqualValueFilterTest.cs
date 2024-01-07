using Hirundo.Commons;
using NUnit.Framework;

namespace Hirundo.Filters.Observations.Tests;

[TestFixture]
public class IsEqualValueFilterTest
{
    [Test]
    public void GivenComplementaryValue_WhenIsAccepteded_ReturnsTrue()
    {
        // Arrange
        var valueName = "SEX";
        var value = "F";
        var filter = new IsEqualValueFilter(valueName, value);

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
        var filter = new IsEqualValueFilter(valueName, value);

        var observation = new Observation(["SPECIES"], ["REG.SCI"]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }
}