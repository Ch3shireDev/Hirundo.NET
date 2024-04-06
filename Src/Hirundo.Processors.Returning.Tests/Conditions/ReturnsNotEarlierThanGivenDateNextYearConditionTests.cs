using Hirundo.Commons.Models;
using Hirundo.Processors.Returning.Conditions;
using NUnit.Framework;

namespace Hirundo.Processors.Returning.Tests.Conditions;

[TestFixture]
public class ReturnsNotEarlierThanGivenDateNextYearConditionTests
{
    [Test]
    public void GivenTwoObservationsWithDistantDates_WhenIsReturning_ReturnsTrue()
    {
        // Arrange
        var filter = new ReturnsNotEarlierThanGivenDateNextYearCondition(06, 01);

        var specimen = new Specimen("AB123", [
            new Observation{Date = new DateTime(2021, 06, 20)},
            new Observation{Date = new DateTime(2022, 06, 01)}
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
        var filter = new ReturnsNotEarlierThanGivenDateNextYearCondition(06, 15);

        var specimen = new Specimen("AB123", [
            new Observation{Date = new DateTime(2021, 06, 01)},
            new Observation{Date = new DateTime(2022, 06, 01)}
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
        var filter = new ReturnsNotEarlierThanGivenDateNextYearCondition(06, 15);

        var specimen = new Specimen("AB123", [
            new Observation{Date = new DateTime(2021, 06, 01)},
            new Observation{Date = new DateTime(2022, 06, 05)},
            new Observation{Date = new DateTime(2023, 06, 15)}
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
        var filter = new ReturnsNotEarlierThanGivenDateNextYearCondition(06, 15);

        var specimen = new Specimen("AB123", [
            new Observation{Date = new DateTime(2021, 06, 01)},
            new Observation{Date = new DateTime(2022, 06, 05)},
            new Observation{Date = new DateTime(2022, 06, 20)}
        ]);

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.False);
    }
}