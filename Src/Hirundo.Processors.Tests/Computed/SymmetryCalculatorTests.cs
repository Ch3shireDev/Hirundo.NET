using Hirundo.Commons.Models;
using Hirundo.Processors.Computed;

namespace Hirundo.Processors.Tests.Computed;

[TestFixture]
public class SymmetryCalculatorTests
{
    [SetUp]
    public void Initialize()
    {
        _calculator = new SymmetryCalculator("SYMMETRY", ["D2", "D3", "D4", "D5", "D6", "D7", "D8"], "WING");
    }

    private SymmetryCalculator _calculator = null!;

    [Test]
    public void GivenBasicCalculator_WhenCalculate_ResultsWithAdditionalColumn()
    {
        // Arrange
        var observation = new Observation(["D2", "D3", "D4", "D5", "D6", "D7", "D8", "WING"], [0, 0, 0, 0, 0, 0, 0, 1]);

        // Act
        var result = _calculator.Calculate(observation);

        // Assert
        Assert.That(result, Is.Not.Null);
        var columns = result.Headers;
        Assert.Multiple(() =>
        {
            Assert.That(columns, Contains.Item("SYMMETRY"));
            Assert.That(result.GetValue("SYMMETRY"), Is.EqualTo(0));
            Assert.That(result, Is.SameAs(observation));
        });
        Assert.That(result.Headers, Has.Count.EqualTo(9));
    }

    [Test]
    public void GivenSymmetricalWing_WhenCalculate_ReturnsZero()
    {
        // Arrange
        var observation = new Observation(["D2", "D3", "D4", "D5", "D6", "D7", "D8", "WING"], [1, 2, 3, 0, 3, 2, 1, 10]);

        // Act
        var result = _calculator.Calculate(observation);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.GetValue("SYMMETRY"), Is.EqualTo(0));
    }

    [Test]
    public void GivenNullValueInWingParameters_WhenCalculate_ReturnsNull()
    {
        // Arrange
        var observation = new Observation(["D2", "D3", "D4", "D5", "D6", "D7", "D8", "WING"], [1, 2, null as int?, 0, 3, 2, 1, 10]);

        // Act
        var result = _calculator.Calculate(observation);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.GetValue("SYMMETRY"), Is.Null);
    }

    [Test]
    public void GivenNullValueInWingLength_WhenCalculate_ReturnsNull()
    {
        // Arrange
        var observation = new Observation(["D2", "D3", "D4", "D5", "D6", "D7", "D8", "WING"], [1, 2, 3, 0, 3, 2, 1, null as int?]);

        // Act
        var result = _calculator.Calculate(observation);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.GetValue("SYMMETRY"), Is.Null);
    }

    [Test]
    public void GivenAsymmetricValues_WhenCalculate_ReturnsNonZeroSymmetry()
    {
        // Arrange
        var observation = new Observation(["D2", "D3", "D4", "D5", "D6", "D7", "D8", "WING"], [1, 2, 3.0, 0, 3, 2m, 4, 10]);

        // Act
        var result = _calculator.Calculate(observation);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.GetValue("SYMMETRY"), Is.EqualTo(0.3m));
    }

    [Test]
    public void GivenValuesAsStrings_WhenCalculate_ReturnsStillCorrectValues()
    {
        // Arrange
        var observation = new Observation(["D2", "D3", "D4", "D5", "D6", "D7", "D8", "WING"], ["1", "2", "3", "0", "3", "2", "4", "10"]);

        // Act
        var result = _calculator.Calculate(observation);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.GetValue("SYMMETRY"), Is.EqualTo(0.3m));
    }

    [Test]
    public void GivenZeroWingLength_WhenCalculate_ReturnsNull()
    {
        // Arrange
        var observation = new Observation(["D2", "D3", "D4", "D5", "D6", "D7", "D8", "WING"], ["1", "2", "3", "0", "3", "2", "4", "0"]);

        // Act
        var result = _calculator.Calculate(observation);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.GetValue("SYMMETRY"), Is.Null);
    }
}