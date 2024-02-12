using Hirundo.Commons;
using NUnit.Framework;

namespace Hirundo.Processors.Computed.Tests
{
    public class SymmetryCalculatorTests
    {
        [Test]
        public void GivenBasicCalculator_WhenCalculate_ResultsWithAdditionalColumn()
        {
            // Arrange
            var observation = new Observation();
            var calculator = new SymmetryCalculator("SYMMETRY", ["D2", "D3", "D4", "D5", "D6", "D7", "D8"], "WING");

            // Act
            var result = calculator.Calculate(observation);

            // Assert
            Assert.That(result, Is.Not.Null);
            var columns = result.GetHeaders();
            Assert.That(columns, Contains.Item("SYMMETRY"));
        }
    }
}
