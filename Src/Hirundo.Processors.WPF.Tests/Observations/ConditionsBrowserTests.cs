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
    private Mock<IObservationsSourceAsync> observationsSource = null!;

    [SetUp]
    public void SetUp()
    {
        labelsRepository = new Mock<ILabelsRepository>();
        speciesRepository = new Mock<ISpeciesRepository>();
        observationsSource = new Mock<IObservationsSourceAsync>();
        browser = new ConditionsBrowser(labelsRepository.Object, speciesRepository.Object, observationsSource.Object);
    }

    [Test]
    public async Task GetSpecimenStatistics_GivenDataSource_ReturnsDataAboutSpecimens()
    {
        // Arrange
        IList<Observation> observations = [
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

        observationsSource.Setup(o => o.GetObservations()).Returns(Task.FromResult(observations));

        // Act
        var specimenStatistics = await browser.GetSpecimenStatistics();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(specimenStatistics.SpecimenCount, Is.EqualTo(2));
            Assert.That(specimenStatistics.SpeciesCount, Is.EqualTo(2));
            Assert.That(specimenStatistics.MaxNumberOfObservationsPerSpecimen, Is.EqualTo(2));
        });
    }

    [Test]
    public async Task GetSpecimenStatistics_GivenThreeSpecimens_ReturnsDataAboutSpecimens()
    {
        // Arrange
        IList<Observation> observations = [
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
            new Observation{
                Ring = "EF789",
                Date = new DateTime(2021, 06, 01),
                Species = "RUB.RUB",
                Headers = ["DATA"],
                Values = ["XYZ"]
            },
        ];

        observationsSource.Setup(o => o.GetObservations()).Returns(Task.FromResult(observations));

        // Act
        var specimenStatistics = await browser.GetSpecimenStatistics();

        // Assert
        Assert.That(specimenStatistics.SpecimenCount, Is.EqualTo(3));
    }

    [Test]
    public async Task GetSpecimenStatistics_GivenThreeSpecies_ReturnsDataAboutSpecimens()
    {
        // Arrange
        IList<Observation> observations = [
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
            new Observation{
                Ring = "EF789",
                Date = new DateTime(2021, 06, 01),
                Species = "ARA.ARA",
                Headers = ["DATA"],
                Values = ["XYZ"]
            }
        ];

        observationsSource.Setup(o => o.GetObservations()).Returns(Task.FromResult(observations));

        // Act
        var specimenStatistics = await browser.GetSpecimenStatistics();

        // Assert
        Assert.That(specimenStatistics.SpeciesCount, Is.EqualTo(3));
    }

    [Test]
    public async Task GetSpecimenStatistics_GivenThreeObservationsForOneSpecimen_ReturnsDataAboutSpecimens()
    {
        // Arrange
        IList<Observation> observations = [
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
                Ring = "AB123",
                Date = new DateTime(2021, 06, 01),
                Species = "REG.REG",
                Headers = ["DATA"],
                Values = ["XYZ"]
            }
        ];

        observationsSource.Setup(o => o.GetObservations()).Returns(Task.FromResult(observations));

        // Act
        var specimenStatistics = await browser.GetSpecimenStatistics();

        // Assert
        Assert.That(specimenStatistics.MaxNumberOfObservationsPerSpecimen, Is.EqualTo(3));
    }

    [Test]
    public async Task GetSpecimenStatistics_GivenMostObservationsForSpecimen_ShowsSpecimenRing()
    {
        // Arrange
        IList<Observation> observations = [
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
                Ring = "XY222",
                Date = new DateTime(2021, 06, 01),
                Species = "REG.REG",
                Headers = ["DATA"],
                Values = ["XYZ"]
            }
        ];

        observationsSource.Setup(o => o.GetObservations()).Returns(Task.FromResult(observations));

        // Act
        var specimenStatistics = await browser.GetSpecimenStatistics();

        // Assert
        Assert.That(specimenStatistics.SpecimenWithMostObservations, Is.EqualTo("AB123"));
    }
}
