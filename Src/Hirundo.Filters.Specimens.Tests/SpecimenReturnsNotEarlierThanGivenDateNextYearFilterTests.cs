using Hirundo.Commons;
using NUnit.Framework;

namespace Hirundo.Filters.Specimens.Tests;

[TestFixture]
public class SpecimenReturnsNotEarlierThanGivenDateNextYearFilterTests
{
    [Test]
    public void GivenTwoObservationsWithDistantDates_WhenIsReturning_ReturnsTrue()
    {
        // Arrange
        var filter = new SpecimenReturnsNotEarlierThanGivenDateNextYearFilter("DATE", 06, 01);

        var specimen = new Specimen
        {
            Identifier = "AB123",
            Observations = [
                new Observation(["DATE"], [DateTime.Parse("2021-06-20")]),
                new Observation(["DATE"], [DateTime.Parse("2022-06-01")]),
            ] 
        };

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenTwoObservationsBeforeDateNextYear_WhenIsReturning_ReturnsFalse()
    {
        // Arrange
        var filter = new SpecimenReturnsNotEarlierThanGivenDateNextYearFilter("DATE", 06, 15);

        var specimen = new Specimen
        {
            Identifier = "AB123",
            Observations = [
                new Observation(["DATE"], [DateTime.Parse("2021-06-01")]),
                new Observation(["DATE"], [DateTime.Parse("2022-06-01")]),
            ] 
        };

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GivenMoreThanTwoObservations_WhenIsReturning_ReturnsTrueIfDistanceIsGreaterThanTimePeriod()
    {
        // Arrange
        var filter = new SpecimenReturnsNotEarlierThanGivenDateNextYearFilter("DATE", 06, 15);

        var specimen = new Specimen
        {
            Identifier = "AB123",
            Observations = [
                new Observation(["DATE"], [DateTime.Parse("2021-06-01")]),
                new Observation(["DATE"], [DateTime.Parse("2022-06-05")]),
                new Observation(["DATE"], [DateTime.Parse("2023-06-15")]),
            ] 
        };

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenMoreThanTwoObservations_WhenIsReturning_ReturnsFalseIfNoPairGivesDistance()
    {
        // Arrange
        var filter = new SpecimenReturnsNotEarlierThanGivenDateNextYearFilter("DATE", 06, 15);

        var specimen = new Specimen
        {
            Identifier = "AB123",
            Observations = [
                new Observation(["DATE"], [DateTime.Parse("2021-06-01")]),
                new Observation(["DATE"], [DateTime.Parse("2022-06-05")]),
                new Observation(["DATE"], [DateTime.Parse("2022-06-20")]),
            ] 
        };

        // Act
        var result = filter.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.False);
    }
}