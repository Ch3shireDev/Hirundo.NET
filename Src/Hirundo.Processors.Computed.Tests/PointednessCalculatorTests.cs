using Hirundo.Commons.Models;
using NUnit.Framework;

namespace Hirundo.Processors.Computed.Tests;

[TestFixture]
public class PointednessCalculatorTests
{
    [SetUp]
    public void Initialize()
    {
        _calculator = new PointednessCalculator("POINTEDNESS", ["D2", "D3", "D4", "D5", "D6", "D7", "D8",], "WING");
    }


    private PointednessCalculator _calculator = null!;

    [Test]
    public void GivenBasicCalculator_WhenCalculate_ResultsWithAdditionalColumn()
    {
        // Arrange
        var observation = new Observation(["D2", "D3", "D4", "D5", "D6", "D7", "D8", "WING"], [0, 0, 0, 0, 0, 0, 0, 1]);

        // Act
        var result = _calculator.Calculate(observation);

        // Assert
        Assert.That(result, Is.Not.Null);
        var columns = result.GetHeaders();
        Assert.That(columns, Contains.Item("POINTEDNESS"));
        Assert.That(result.GetValue("POINTEDNESS"), Is.EqualTo(0));
        Assert.That(result, Is.SameAs(observation));
        Assert.That(result.GetHeaders().Count, Is.EqualTo(9));
    }

    [Test]
    public void GivenObservationWithSimpleValues_WhenCalculate_ResultIsSumOfWingParametersDividedByWingLength()
    {
        // Arrange
        var observation = new Observation(["D2", "D3", "D4", "D5", "D6", "D7", "D8", "WING"], [1, 2, 3, 4, 5, 6, 7, 10m]);

        // Act
        var result = _calculator.Calculate(observation);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.GetValue("POINTEDNESS"), Is.EqualTo(2.8m));
    }

    [Test]
    public void GivenObservationValuesAsStrings_WhenCalculate_StillResultsWithCorrectValue()
    {
        // Arrange
        var observation = new Observation(["D2", "D3", "D4", "D5", "D6", "D7", "D8", "WING"], ["5", "3", "2", "1", "2", "3", "4", "5.0"]);

        // Act
        var result = _calculator.Calculate(observation);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.GetValue("POINTEDNESS"), Is.EqualTo(4.0m));
    }
}
