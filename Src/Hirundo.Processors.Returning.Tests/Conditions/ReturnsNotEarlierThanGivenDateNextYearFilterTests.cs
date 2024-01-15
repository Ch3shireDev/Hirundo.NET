using System.Globalization;
using Hirundo.Commons;
using Hirundo.Processors.Returning.Conditions;
using NUnit.Framework;

namespace Hirundo.Processors.Returning.Tests.Conditions;

[TestFixture]
public class ReturnsNotEarlierThanGivenDateNextYearFilterTests
{
    [Test]
    public void GivenTwoObservationsWithDistantDates_WhenIsReturning_ReturnsTrue()
    {
        // Arrange
        var filter = new ReturnsNotEarlierThanGivenDateNextYearFilter("DATE", 06, 01);

        var specimen = new Specimen("AB123", [
            new Observation(["DATE"], [DateTime.Parse("2021-06-20", CultureInfo.InvariantCulture)]),
            new Observation(["DATE"], [DateTime.Parse("2022-06-01", CultureInfo.InvariantCulture)])
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenTwoObservationsBeforeDateNextYear_WhenIsReturning_ReturnsFalse()
    {
        // Arrange
        var filter = new ReturnsNotEarlierThanGivenDateNextYearFilter("DATE", 06, 15);

        var specimen = new Specimen("AB123", [
            new Observation(["DATE"], [DateTime.Parse("2021-06-01", CultureInfo.InvariantCulture)]),
            new Observation(["DATE"], [DateTime.Parse("2022-06-01", CultureInfo.InvariantCulture)])
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GivenMoreThanTwoObservations_WhenIsReturning_ReturnsTrueIfDistanceIsGreaterThanTimePeriod()
    {
        // Arrange
        var filter = new ReturnsNotEarlierThanGivenDateNextYearFilter("DATE", 06, 15);

        var specimen = new Specimen("AB123", [
            new Observation(["DATE"], [DateTime.Parse("2021-06-01", CultureInfo.InvariantCulture)]),
            new Observation(["DATE"], [DateTime.Parse("2022-06-05", CultureInfo.InvariantCulture)]),
            new Observation(["DATE"], [DateTime.Parse("2023-06-15", CultureInfo.InvariantCulture)])
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenMoreThanTwoObservations_WhenIsReturning_ReturnsFalseIfNoPairGivesDistance()
    {
        // Arrange
        var filter = new ReturnsNotEarlierThanGivenDateNextYearFilter("DATE", 06, 15);

        var specimen = new Specimen("AB123", [
            new Observation(["DATE"], [DateTime.Parse("2021-06-01", CultureInfo.InvariantCulture)]),
            new Observation(["DATE"], [DateTime.Parse("2022-06-05", CultureInfo.InvariantCulture)]),
            new Observation(["DATE"], [DateTime.Parse("2022-06-20", CultureInfo.InvariantCulture)])
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.False);
    }
}