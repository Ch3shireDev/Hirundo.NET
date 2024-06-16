using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Processors.WPF.Observations;
using Moq;

namespace Hirundo.Processors.WPF.Tests.Observations;

[TestFixture]
public class ConditionsBrowserTests
{
    private ConditionsBrowser browser = null!;
    private Mock<ILabelsRepository> labelsRepository = null!;
    private Mock<ISpeciesRepository> speciesRepository = null!;

    [SetUp]
    public void SetUp()
    {
        labelsRepository = new Mock<ILabelsRepository>();
        speciesRepository = new Mock<ISpeciesRepository>();
        browser = new ConditionsBrowser(labelsRepository.Object, speciesRepository.Object);
    }

    [Test]
    public void GetSpecimenStatistics_GivenDataSource_ReturnsDataAboutSpecimens()
    {
        // Arrange
        Observation[] observations = [
            new Observation{
                Ring = "AB123",
                Date = new DateTime(2021, 06, 01),
                Species = "REG.REG",
                Headers = ["DATA"],
                Values = ["XYZ"]
            },
            new Observation{
                Ring = "AB123",
                Date = new DateTime(2021, 06, 01),
                Species = "REG.REG",
                Headers = ["DATA"],
                Values = ["XYZ"]
            },
            new Observation{
                Ring = "CD456",
                Date = new DateTime(2021, 06, 01),
                Species = "RUB.RUB",
                Headers = ["DATA"],
                Values = ["XYZ"]
            },
        ];

        // Act
        var specimenStatistics = browser.GetSpecimenStatistics();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(specimenStatistics.SpecimenCount, Is.EqualTo(2));
            Assert.That(specimenStatistics.SpeciesCount, Is.EqualTo(2));
            Assert.That(specimenStatistics.MaxNumberOfObservationsPerSpecimen, Is.EqualTo(2));
        });
    }
}
