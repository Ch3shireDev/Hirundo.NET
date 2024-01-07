using Hirundo.Commons;
using NUnit.Framework;

namespace Hirundo.Filters.Observations.Tests;

[TestFixture]
public class IsInSeasonFilterTests
{
    [Test]
    public void GivenInSeason_WhenIsSelect_ReturnsTrue()
    {
        // Arrange
        var season = new Season(06, 01, 08, 31);
        var filter = new IsInSeasonFilter("DATE", season);
        var observation = new Observation(["DATE"], [DateTime.Parse("2021-06-01")]);

        // Act
        var result = filter.IsSelected(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenOutsideSeason_WhenIsSelect_ReturnsFalse()
    {
        // Arrange
        var season = new Season(06, 15, 08, 15);
        var filter = new IsInSeasonFilter("DATE", season);
        var observation = new Observation(["DATE"], [DateTime.Parse("2021-05-16")]);

        // Act
        var result = filter.IsSelected(observation);

        // Assert
        Assert.That(result, Is.False);
    }
}