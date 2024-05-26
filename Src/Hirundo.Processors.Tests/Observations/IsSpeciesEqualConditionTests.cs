using Hirundo.Commons.Models;
using Hirundo.Processors.Observations;
using System.Text;

namespace Hirundo.Processors.Tests.Observations;

[TestFixture]
public class IsSpeciesEqualConditionTests
{
    [Test]
    public void GivenSpecies_WhenIsAccepted_ReturnsOnlyObservationsWithGivenSpecies()
    {
        // Arrange
        var speciesBuilder = new StringBuilder();
        speciesBuilder.Append("REG").Append(".REG");
        var condition = new IsSpeciesEqualCondition { Species = speciesBuilder.ToString() };

        Observation[] observations =
        [
            new Observation { Species = "REG.REG" },
            new Observation { Species = "PHY.LUS" }
        ];

        // Act
        var result = observations.Where(condition.IsAccepted).ToArray();

        // Assert
        Assert.That(result, Has.Length.EqualTo(1));
        Assert.That(result[0].Species, Is.EqualTo("REG.REG"));
    }
}