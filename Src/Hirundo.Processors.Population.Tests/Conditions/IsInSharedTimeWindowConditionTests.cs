using Hirundo.Commons.Models;
using Hirundo.Processors.Population.Conditions;
using NUnit.Framework;

namespace Hirundo.Processors.Population.Tests.Conditions;

[TestFixture]
public class IsInSharedTimeWindowConditionTests
{
    [Test]
    public void GivenSpecimenFromTimeWindow_WhenIsAccepted_ReturnsTrue()
    {
        var returningSpecimen = new Specimen("XXX", [new Observation { Date = new DateTime(2020, 06, 01) }]);

        var days = 20;

        var specimen = new Specimen("ABC123", [new Observation { Date = new DateTime(2020, 05, 20) }]);

        var condition = new IsInSharedTimeWindowConditionBuilder(days);
        var @internal = condition.GetPopulationCondition(returningSpecimen);

        // Act
        var result = @internal.IsAccepted(specimen);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void GivenSpecimenOutsideTimeWindow_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange
        var returningSpecimen = new Specimen("XX123", [
                new Observation{Date = new DateTime(2020, 06, 01)},
                new Observation{Date = new DateTime(2021, 06, 01)}
            ]
        );

        var days = 20;

        var specimen = new Specimen("ABC123", [
            new Observation{Date = new DateTime(2020, 05, 05)}
            ]);

        var filterBuilder = new IsInSharedTimeWindowConditionBuilder(days);
        var filter = filterBuilder.GetPopulationCondition(returningSpecimen);

        // Act
        var result = filter.IsAccepted(specimen);

        // Assert
        Assert.That(result, Is.False);
    }
}