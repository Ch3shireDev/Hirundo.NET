using Hirundo.Commons.Models;
using Hirundo.Processors.Population.Conditions;
using NUnit.Framework;

namespace Hirundo.Processors.Population.Tests;

public class IsEqualConditionTests
{
    [Test]
    public void GivenSpecimens_WhenIsEqual_ReturnsOnlySpecimensThatHaveValuesEqual()
    {
        // Arrange
        var valueName = "STATUS";
        var value = "O";

        var specimens = new[]
        {
            new Specimen("AB123",[ new Observation(["STATUS"], ["O"]) ]),
            new Specimen("CD456",[ new Observation(["STATUS"], ["X"]) ]),
        };

        var returningSpecimen = new Specimen("EF789", [new Observation(["STATUS"], ["O"])]);

        // Act
        var condition = new IsEqualPopulationCondition { ValueName = valueName, Value = value };
        var conditionClosure = condition.GetPopulationConditionClosure(returningSpecimen);
        var result = specimens.Where(conditionClosure.IsAccepted).ToArray();

        // Assert
        Assert.That(result.Length, Is.EqualTo(1));
        Assert.That(result[0].Ring, Is.EqualTo("AB123"));
    }
}