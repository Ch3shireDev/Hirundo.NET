using Hirundo.Commons.Models;
using Hirundo.Processors.Observations;

namespace Hirundo.Processors.Tests.Observations;

[TestFixture]
public class IsInSeasonConditionTests
{
    [Test]
    public void GivenInSeason_WhenIsAccepted_ReturnsTrue()
    {
        // Arrange
        var season = new Season(06, 01, 08, 31);
        var filter = new IsInSeasonCondition(season);

        var observation = new Observation
        {
            Date = new DateTime(2021, 06, 01)
        };

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
        var filter = new IsInSeasonCondition(season);

        var observation = new Observation
        {
            Date = new DateTime(2021, 05, 16)
        };

        // Act
        var result = filter.IsAccepted(observation);

        // Assert
        Assert.That(result, Is.False);
    }
}