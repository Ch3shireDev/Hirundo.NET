using Hirundo.Commons.Models;
using Hirundo.Processors.Returning.Conditions;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Returning.Tests.Conditions;

[TestFixture]
public class AlternativeReturningConditionTests
{
    [Test]
    [TestCase(true, true)]
    [TestCase(false, false)]
    public void GivenOneCondition_WhenIsReturning_ThenReturnsValueOfThisCondition(bool returnValue, bool expectedResult)
    {
        // Arrange
        var condition = new Mock<IReturningSpecimenCondition>();
        condition.Setup(c => c.IsReturning(It.IsAny<Specimen>())).Returns(returnValue);
        var alternativeCondition = new AlternativeReturningCondition(condition.Object);
        var specimen = new Specimen("AB123", [new Observation(["IS_RETURNING"], [true])]);

        // Act
        var result = alternativeCondition.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    [TestCase(true, true, true)]
    [TestCase(true, false, true)]
    [TestCase(false, true, true)]
    [TestCase(false, false, false)]
    public void GivenTwoConditions_WhenIsReturning_ThenReturnsValueOfFirstTrueCondition(bool firstReturnValue, bool secondReturnValue, bool expectedResult)
    {
        // Arrange
        var firstCondition = new Mock<IReturningSpecimenCondition>();
        firstCondition.Setup(c => c.IsReturning(It.IsAny<Specimen>())).Returns(firstReturnValue);
        var secondCondition = new Mock<IReturningSpecimenCondition>();
        secondCondition.Setup(c => c.IsReturning(It.IsAny<Specimen>())).Returns(secondReturnValue);
        var alternativeCondition = new AlternativeReturningCondition(firstCondition.Object, secondCondition.Object);
        var specimen = new Specimen("AB123", [new Observation(["IS_RETURNING"], [true])]);

        // Act
        var result = alternativeCondition.IsReturning(specimen);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
