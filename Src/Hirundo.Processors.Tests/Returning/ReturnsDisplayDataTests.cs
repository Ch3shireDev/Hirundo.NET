using Hirundo.Commons.Models;

namespace Hirundo.Processors.Tests.Returning;

[TestFixture]
public class ReturnsDisplayDataTests
{
    [Test]
    public void ReturningDataShowsOnlyFirstRecord()
    {
        // Arrange
        var observations = new List<Observation>
        {
            new Observation { Date = new DateTime(2021, 06, 01), Species="RUB.RUB", Ring="AB123", Headers=["D1", "D2"], Values=[1,2] },
            new Observation { Date = new DateTime(2021, 06, 02), Species="RUB.RUB", Ring="AB123", Headers=["D1", "D2"], Values=[3,4] },
        };

        // Act
        var specimen = new Specimen("AB123", observations);

        // Assert
        Assert.That(specimen.GetValue("D1"), Is.EqualTo(1));
        Assert.That(specimen.GetValue("D2"), Is.EqualTo(2));
    }

    [Test]
    public void ReturningDataShowsFirstRecordedObservation()
    {
        // Arrange
        var observations = new List<Observation>
        {
            new Observation { Date = new DateTime(2021, 06, 02), Species="RUB.RUB", Ring="AB123", Headers=["D1", "D2"], Values=[3,4] },
            new Observation { Date = new DateTime(2021, 06, 01), Species="RUB.RUB", Ring="AB123", Headers=["D1", "D2"], Values=[5,6] },
        };

        // Act
        var specimen = new Specimen("AB123", observations);

        // Assert
        Assert.That(specimen.GetValue("D1"), Is.EqualTo(5));
        Assert.That(specimen.GetValue("D2"), Is.EqualTo(6));
    }
}
