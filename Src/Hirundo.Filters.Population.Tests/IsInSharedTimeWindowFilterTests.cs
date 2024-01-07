using Hirundo.Commons;
using NUnit.Framework;

namespace Hirundo.Filters.Population.Tests;

[TestFixture]
public class IsInSharedTimeWindowFilterTests
{
    [Test]
    public void GivenSpecimenFromTimeWindow_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        var returningSpecimen = new Specimen
        {
            Observations =
            [
                new Observation(["DATE"], [DateTime.Parse("2020-06-01")]),
                new Observation(["DATE"], [DateTime.Parse("2021-06-01")])
            ]
        };

        var dateValueName = "DATE";
        var days = 20;

        var specimen = new Specimen
        {
            Identifier = "ABC123",
            Observations = [new Observation(["DATE"], [DateTime.Parse("2020-05-20")])]
        };

        var filter = new IsInSharedTimeWindowFilter(returningSpecimen, dateValueName, days);

        // Act
        var result = filter.IsAccepted(specimen);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenSpecimenOutsideTimeWindow_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange
        var returningSpecimen = new Specimen
        {
            Observations =
            [
                new Observation(["DATE"], [DateTime.Parse("2020-06-01")]),
                new Observation(["DATE"], [DateTime.Parse("2021-06-01")])
            ]
        };

        var dateValueName = "DATE";
        var days = 20;

        var specimen = new Specimen
        {
            Identifier = "ABC123",
            Observations = [new Observation(["DATE"], [DateTime.Parse("2020-05-05")])]
        };

        var filter = new IsInSharedTimeWindowFilter(returningSpecimen, dateValueName, days);

        // Act
        var result = filter.IsAccepted(specimen);

        // Assert
        Assert.That(result, Is.False);
    }
}