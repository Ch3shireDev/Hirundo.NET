using Hirundo.Commons.Models;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Tests.Population;

[TestFixture]
public class IsNotMainSpecimenPopulationConditionTests
{
    [Test]
    public void GivenMainSpecimen_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange
        var returningSpecimen = new Specimen("XXX", [new Observation { Date = new DateTime(2020, 06, 01) }]);

        var specimen = new Specimen("ABC123", [new Observation { Date = new DateTime(2020, 05, 20) }]);

        var condition = new IsNotMainSpecimenPopulationCondition();
        var internal1 = condition.GetPopulationConditionClosure(returningSpecimen);
        var internal2 = condition.GetPopulationConditionClosure(specimen);

        // Act
        var returningSpecimenResult1 = internal1.IsAccepted(returningSpecimen);
        var otherSpecimenResult1 = internal1.IsAccepted(specimen);
        var returningSpecimenResult2 = internal2.IsAccepted(returningSpecimen);
        var otherSpecimenResult2 = internal2.IsAccepted(specimen);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(returningSpecimenResult1, Is.EqualTo(false));
            Assert.That(otherSpecimenResult1, Is.EqualTo(true));
            Assert.That(returningSpecimenResult2, Is.EqualTo(true));
            Assert.That(otherSpecimenResult2, Is.EqualTo(false));
        });
    }
}