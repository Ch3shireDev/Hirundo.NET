using Hirundo.Commons;
using Hirundo.Databases;
using Hirundo.Writers.Summary;
using Moq;
using NUnit.Framework;

namespace Hirundo.App.Tests.Integration;

public class HirundoAppTests
{
    private HirundoApp _app = null!;
    private Mock<IDatabase> _database = null!;
    private Mock<IDatabaseBuilder> _databaseBuilder = null!;

    private Mock<ISummaryWriterBuilder> _summaryWriterBuilder = null!;
    private Mock<ISummaryWriter> _summaryWriter = null!;

    [SetUp]
    public void Initialize()
    {
        _database = new Mock<IDatabase>();
        _databaseBuilder = new Mock<IDatabaseBuilder>();

        _databaseBuilder
            .Setup(x => x.WithDatabaseParameters(It.IsAny<IList<IDatabaseParameters>>(), It.IsAny<CancellationToken?>()))
            .Returns(_databaseBuilder.Object);

        _databaseBuilder
            .Setup(x => x.Build())
            .Returns(_database.Object);

        _summaryWriter = new Mock<ISummaryWriter>();

        _summaryWriterBuilder = new Mock<ISummaryWriterBuilder>();

        _summaryWriterBuilder
            .Setup(x => x.Build())
            .Returns(_summaryWriter.Object);

        _summaryWriterBuilder
            .Setup(x => x.WithWriterParameters(It.IsAny<IWriterParameters>()))
            .Returns(_summaryWriterBuilder.Object);

        _app = new HirundoApp(_databaseBuilder.Object, _summaryWriterBuilder.Object);
    }

    [Test]
    public void GivenEmptyObservationsList_WhenRun_ResultsInEmptySummary()
    {
        // Arrange
        _database.Setup(x => x.GetObservations()).Returns([]);

        // Act
        _app.Run(new ApplicationConfig());

        // Assert
        _database.Verify(x => x.GetObservations(), Times.Once);
        _summaryWriter.Verify(x => x.Write(It.IsAny<IEnumerable<ReturningSpecimenSummary>>()), Times.Once);
    }
}