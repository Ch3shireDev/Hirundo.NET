using Hirundo.Commons;
using Moq;
using NUnit.Framework;

namespace Hirundo.Processors.Computed.Tests;

[TestFixture]
public class CompositeCalculatorTests
{
    [SetUp]
    public void Initialize()
    {
        _calculator1 = new Mock<IComputedValuesCalculator>();
        _calculator2 = new Mock<IComputedValuesCalculator>();

        _calculator = new CompositeCalculator(_calculator1.Object, _calculator2.Object);
    }

    private Mock<IComputedValuesCalculator> _calculator1 = null!;
    private Mock<IComputedValuesCalculator> _calculator2 = null!;

    private CompositeCalculator _calculator = null!;

    [Test]
    public void GivenBasicCalculator_WhenCalculate_ResultsWithAdditionalColumn()
    {
        // Arrange
        var observation1 = new Observation();
        var observation2 = new Observation();
        var observation3 = new Observation();
        _calculator1.Setup(x => x.Calculate(observation1)).Returns(observation2);
        _calculator2.Setup(x => x.Calculate(observation2)).Returns(observation3);

        // Act
        var result = _calculator.Calculate(observation1);

        // Assert
        Assert.That(result, Is.EqualTo(observation3));
        _calculator1.Verify(x => x.Calculate(observation1), Times.Once);
        _calculator2.Verify(x => x.Calculate(observation2), Times.Once);
    }
}