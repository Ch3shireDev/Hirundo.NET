using Hirundo.Commons;
using NUnit.Framework;

namespace Hirundo.Filters.Observations.Tests;

[TestFixture]
public class IsNotEqualFilterTests
{
    [Test]
    public void GivenOtherValue_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        var valueName = "SEX";
        var value = "F";

        var filter = new IsNotEqualFilter(valueName, value);

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

        var filter = new IsNotEqualFilter(valueName, value);

        var observation = new Observation(["SPECIES"], ["REG.REG"]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }
}