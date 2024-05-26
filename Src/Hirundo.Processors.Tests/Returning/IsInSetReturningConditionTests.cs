using Hirundo.Commons.Models;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Tests.Returning;

[TestFixture]
public class IsInSetReturningConditionTests
{
    [Test]
    public void GivenSpecimenWithValueInSet_WhenIsReturning_ReturnsTrue()
    {
        // Arrange
        var observations = new List<Observation>
        {
            new(["ABC"], ["1"])
        };
        var specimen = new Specimen("R123", observations);

        var condition = new IsInSetReturningCondition("ABC", new List<object> { "1", "2", "3" });

        // Act
        var result = condition.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenSpecimenWithValueOutsideSet_WhenIsReturning_ReturnsFalse()
    {
        // Arrange
        var observations = new List<Observation>
        {
            new(["D2"], [2])
        };
        var specimen = new Specimen("R123", observations);

        var condition = new IsInSetReturningCondition("D2", new List<object> { 3, 4, 5 });

        // Act
        var result = condition.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GivenAnyObservationFromSet_WhenIsReturning_ReturnsTrue()
    {
        // Arrange
        var observations = new List<Observation>
        {
            new(["D2"], [2]),
            new(["D2"], [3]),
            new(["D2"], [4]),
            new(["D2"], [5])
        };

        var specimen = new Specimen("R123", observations);

        var condition = new IsInSetReturningCondition("D2", new List<object> { 5, 6, 7 });

        // Act
        var result = condition.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.True);
    }
}