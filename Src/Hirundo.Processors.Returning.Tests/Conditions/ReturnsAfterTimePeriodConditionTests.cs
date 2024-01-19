using System.Globalization;
using Hirundo.Commons;
using Hirundo.Processors.Returning.Conditions;
using NUnit.Framework;

namespace Hirundo.Processors.Returning.Tests.Conditions;

[TestFixture]
public class ReturnsAfterTimePeriodConditionTests
{
    [Test]
    public void GivenTwoObservationsWithDistantDates_WhenIsReturning_ReturnsTrue()
    {
        // Arrange
        var filter = new ReturnsAfterTimePeriodCondition("DATE", 20);

        var specimen = new Specimen("AB123", [
            new Observation(["DATE"], [DateTime.Parse("2021-06-01", CultureInfo.InvariantCulture)]),
            new Observation(["DATE"], [DateTime.Parse("2021-06-21", CultureInfo.InvariantCulture)])
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
        var filter = new ReturnsAfterTimePeriodCondition("DATE", 20);

        var specimen = new Specimen("AB123", [
            new Observation(["DATE"], [DateTime.Parse("2021-06-01", CultureInfo.InvariantCulture)]),
            new Observation(["DATE"], [DateTime.Parse("2021-06-05", CultureInfo.InvariantCulture)]),
            new Observation(["DATE"], [DateTime.Parse("2021-06-27", CultureInfo.InvariantCulture)])
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
        var filter = new ReturnsAfterTimePeriodCondition("DATE", 20);

        var specimen = new Specimen("AB123",
        [
            new Observation(["DATE"], [DateTime.Parse("2021-06-01", CultureInfo.InvariantCulture)]),
            new Observation(["DATE"], [DateTime.Parse("2021-06-05", CultureInfo.InvariantCulture)]),
            new Observation(["DATE"], [DateTime.Parse("2021-06-22", CultureInfo.InvariantCulture)])
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.False);
    }
}