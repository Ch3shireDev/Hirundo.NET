using Hirundo.Commons;
using NUnit.Framework;

namespace Hirundo.Filters.Specimens.Tests;

[TestFixture]
public class ReturnsAfterTimePeriodFilterTests
{
    [Test]
    public void GivenTwoObservationsWithDistantDates_WhenIsReturning_ReturnsTrue()
    {
        // Arrange
        var filter = new ReturnsAfterTimePeriodFilter("DATE", 20);

        var specimen = new Specimen("AB123", [
            new Observation(["DATE"], [DateTime.Parse("2021-06-01")]),
            new Observation(["DATE"], [DateTime.Parse("2021-06-21")])
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenMoreThanTwoObservationsWithDistantDates_WhenIsReturning_ReturnsTrue()
    {
        // Arrange
        var filter = new ReturnsAfterTimePeriodFilter("DATE", 20);

        var specimen = new Specimen("AB123", [
            new Observation(["DATE"], [DateTime.Parse("2021-06-01")]),
            new Observation(["DATE"], [DateTime.Parse("2021-06-05")]),
            new Observation(["DATE"], [DateTime.Parse("2021-06-27")])
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenMoreThanTwoObservationsWithDistanceLessThanTimePeriod_WhenIsReturning_ReturnsFalse()
    {
        // Arrange
        var filter = new ReturnsAfterTimePeriodFilter("DATE", 20);

        var specimen = new Specimen("AB123",
        [
            new Observation(["DATE"], [DateTime.Parse("2021-06-01")]),
            new Observation(["DATE"], [DateTime.Parse("2021-06-05")]),
            new Observation(["DATE"], [DateTime.Parse("2021-06-22")])
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.False);
    }
}