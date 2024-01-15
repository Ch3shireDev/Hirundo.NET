using System.Globalization;
using Hirundo.Commons;
using Hirundo.Processors.Observations.Conditions;
using NUnit.Framework;

namespace Hirundo.Processors.Observations.Tests.Conditions;

[TestFixture]
public class IsInSeasonFilterTests
{
    [Test]
    public void GivenInSeason_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        var season = new Season(06, 01, 08, 31);
        var filter = new IsInSeasonFilter("DATE", season);
        var observation = new Observation(["DATE"], [DateTime.Parse("2021-06-01", CultureInfo.InvariantCulture)]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenOutsideSeason_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange
        var season = new Season(06, 15, 08, 15);
        var filter = new IsInSeasonFilter("DATE", season);
        var observation = new Observation(["DATE"], [DateTime.Parse("2021-05-16", CultureInfo.InvariantCulture)]);

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }
}