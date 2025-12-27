using Hirundo.Commons.Models;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Tests.Population;

[TestFixture]
public class IsPopulationInDateRangeConditionTests
{
    [Test]
    public void GivenMainSpecimen_WhenIsAccepted_ReturnsFalse()
    {
        // Arrange 
        var specimen1 = new Specimen("ABC123", [new Observation { Date = new DateTime(2020, 09, 20) }]);
        var specimen2 = new Specimen("ABC123", [new Observation { Date = new DateTime(2020, 10, 20) }]);

        var condition = new IsPopulationInDateRangeCondition(); 
        var internal2 = condition.GetPopulationConditionClosure(specimen1);
        var internal3 = condition.GetPopulationConditionClosure(specimen2);

        // Act 
        var otherSpecimenResult1 = internal2.IsAccepted(specimen1); 
        var otherSpecimenResult2 = internal3.IsAccepted(specimen2);

        Assert.Multiple(() =>
        {
            // Assert 
            Assert.That(otherSpecimenResult1, Is.EqualTo(true)); 
            Assert.That(otherSpecimenResult2, Is.EqualTo(false));
        });
    }
}