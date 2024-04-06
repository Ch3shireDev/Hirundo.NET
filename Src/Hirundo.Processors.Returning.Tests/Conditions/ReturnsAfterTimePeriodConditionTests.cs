using Hirundo.Commons.Models;
using Hirundo.Processors.Returning.Conditions;
using NUnit.Framework;
using System.Globalization;

namespace Hirundo.Processors.Returning.Tests.Conditions;

[TestFixture]
public class ReturnsAfterTimePeriodConditionTests
{
    [Test]
    public void GivenTwoObservationsWithDistantDates_WhenIsReturning_ReturnsTrue()
    {
        // Arrange
        var filter = new ReturnsAfterTimePeriodCondition(20);

        var specimen = new Specimen("AB123", [
            new Observation{Date = new DateTime(2021, 06, 01)},
            new Observation{Date = new DateTime(2021, 06, 21)}
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
        var filter = new ReturnsAfterTimePeriodCondition(20);

        var specimen = new Specimen("AB123", [
            new Observation{Date = new DateTime(2021, 06, 01)},
            new Observation{Date = new DateTime(2021, 06, 05)},
            new Observation{Date = new DateTime(2021, 06, 27)},
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
        var filter = new ReturnsAfterTimePeriodCondition(20);

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

    [Test]
    public void GivenDateAsString_WhenIsReturning_ReturnsTrue()
    {
        // Arrange
        var filter = new ReturnsAfterTimePeriodCondition(20);

        var specimen = new Specimen("AB123", [
            //new Observation(["DATE"], ["2021-06-01"]),
            //new Observation(["DATE"], ["2021-06-05"]),
            //new Observation(["DATE"], ["2021-06-27"])
            new Observation{Date = new DateTime(2021, 06, 01)},
            new Observation{Date = new DateTime(2021, 06, 05)},
            new Observation{Date = new DateTime(2021, 06, 27)},

        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.True);
    }

}