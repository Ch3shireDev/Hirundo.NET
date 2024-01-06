using Hirundo.Commons;
using NUnit.Framework;

namespace Hirundo.Filters.Observations.Tests;

[TestFixture]
public class OnlyGivenValueFilterTest
{
    [Test]
    public void GivenComplementaryValue_WhenIsSelected_ReturnsTrue()
    {
        // Arrange
        var valueName = "SEX";
        var value = "F";
        var filter = new OnlyGivenValueFilter(valueName, value);

        var observation = new Observation(["SEX"], ["F"]);

        // Act
        var result = filter.IsSelected(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenWrongValue_WhenIsSelected_ReturnsFalse()
    {
        // Arrange
        var valueName = "SPECIES";
        var value = "REG.REG";

        var filter = new OnlyGivenValueFilter(valueName, value);

        var observation = new Observation(["SPECIES"], ["REG.SCI"]);

        // Act
        var result = filter.IsSelected(observation);

        // Assert
        Assert.That(result, Is.False);
    }
}