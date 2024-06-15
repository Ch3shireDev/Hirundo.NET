using Hirundo.Commons.Models;

namespace Hirundo.Databases.Tests.Integration;

[TestFixture]
public class XlsxDatabaseIntegrationTests
{
    private ExcelDatabaseParameters parameters = null!;
    private DatabaseParameters databaseParameters = null!;

    [SetUp]
    public void Initialize()
    {
        parameters = new ExcelDatabaseParameters
        {
            Path = "./Assets/example-data.xlsx",
            RingIdentifier = "RING",
            DateIdentifier = "DATE",
            SpeciesIdentifier = "SPECIES",
            Columns =
            [
                new ColumnParameters {
                    DatabaseColumn = "ID", ValueName = "ID", DataType = DataType.Number },
                new ColumnParameters {
                    DatabaseColumn = "RING", ValueName = "RING", DataType = DataType.Text },
                new ColumnParameters {
                    DatabaseColumn = "SPECIES", ValueName = "SPECIES", DataType = DataType.Text },
                new ColumnParameters {
                    DatabaseColumn = "DATE", ValueName = "DATE", DataType = DataType.Date },
                new ColumnParameters {
                    DatabaseColumn = "HOUR", ValueName = "HOUR", DataType = DataType.Number },
                new ColumnParameters {
                    DatabaseColumn = "SEX", ValueName = "SEX", DataType = DataType.Text },
                new ColumnParameters {
                    DatabaseColumn = "AGE", ValueName = "AGE", DataType = DataType.Text },
                new ColumnParameters {
                    DatabaseColumn = "WEIGHT", ValueName = "WEIGHT", DataType = DataType.Numeric },
                new ColumnParameters {
                    DatabaseColumn = "STATUS", ValueName = "STATUS", DataType = DataType.Text },
                new ColumnParameters {
                    DatabaseColumn = "FAT", ValueName = "FAT", DataType = DataType.Number }
            ]
        };

        databaseParameters = new DatabaseParameters
        {
            Databases = [parameters]
        };
    }

    [Test]
    public void Explain_ReturnsStringWithDatabaseParameters()
    {
        // Arrange
        var expected = "Dane pobrano z pliku ./Assets/example-data.xlsx. Kolumna obrączki: RING, kolumna daty: DATE. Liczba kolumn: 10." + Environment.NewLine +
            "Kolumny:" + Environment.NewLine +
            " - Kolumna ID jako ID, typu Number." + Environment.NewLine +
            " - Kolumna RING jako RING, typu Text." + Environment.NewLine +
            " - Kolumna SPECIES jako SPECIES, typu Text." + Environment.NewLine +
            " - Kolumna DATE jako DATE, typu Date." + Environment.NewLine +
            " - Kolumna HOUR jako HOUR, typu Number." + Environment.NewLine +
            " - Kolumna SEX jako SEX, typu Text." + Environment.NewLine +
            " - Kolumna AGE jako AGE, typu Text." + Environment.NewLine +
            " - Kolumna WEIGHT jako WEIGHT, typu Numeric." + Environment.NewLine +
            " - Kolumna STATUS jako STATUS, typu Text." + Environment.NewLine +
            " - Kolumna FAT jako FAT, typu Number." + Environment.NewLine
            ;

        // Act
        var result = parameters.Explain();

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Build_ReturnsDatabaseWithCorrectParameters()
    {
        // Arrange
        var builder = new DatabaseBuilder();

        // Act
        var result = builder.WithDatabaseParameters([parameters]).Build();

        // Assert
        Assert.That(result, Is.InstanceOf<IDatabase>());
        Assert.That(result, Is.InstanceOf<XlsxDatabase>());
    }

    [Test]
    public void Datasource_ReturnsCorrectValues()
    {
        // Arrange
        var datasource = databaseParameters.BuildDataSource();

        // Act
        var result = datasource.GetObservations().ToArray();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.EqualTo(3));

        var expectedHeaders = new[] { "ID", "RING", "SPECIES", "DATE", "HOUR", "SEX", "AGE", "WEIGHT", "STATUS", "FAT" };

        Assert.That(result[0].Ring, Is.EqualTo("H137652"));
        Assert.That(result[0].Species, Is.EqualTo("RUB.RUB"));
        Assert.That(result[0].Date, Is.EqualTo(new DateTime(1967, 08, 17)));
        Assert.That(result[0].Headers, Is.EquivalentTo(expectedHeaders));
        object?[] expectedValues1 = [4610, "H137652", "RUB.RUB", new DateTime(1967, 08, 17), 6, "F", "J", 15.3, "O", 1];
        Assert.That(result[0].Values, Is.EquivalentTo(expectedValues1));

        Assert.That(result[1].Ring, Is.EqualTo("H137674"));
        Assert.That(result[1].Species, Is.EqualTo("ERI.RUB"));
        Assert.That(result[1].Date, Is.EqualTo(new DateTime(1967, 08, 17)));
        Assert.That(result[1].Headers, Is.EquivalentTo(expectedHeaders));
        object?[] expectedValues2 = [4632, "H137674", "ERI.RUB", new DateTime(1967, 08, 17), 19, "M", "J", 18.2, "R", 2];
        Assert.That(result[1].Values, Is.EquivalentTo(expectedValues2));

        Assert.That(result[2].Ring, Is.EqualTo("H137674"));
        Assert.That(result[2].Species, Is.EqualTo("ERI.RUB"));
        Assert.That(result[2].Date, Is.EqualTo(new DateTime(1967, 08, 18)));
        Assert.That(result[2].Headers, Is.EquivalentTo(expectedHeaders));
        object?[] expectedValues3 = [4655, "H137674", "ERI.RUB", new DateTime(1967, 08, 18), 9, null, "L", null, "R", null];
        Assert.That(result[2].Values, Is.EquivalentTo(expectedValues3));
    }

    [Test]
    public void Datasource_ReturnsCorrectTypes()
    {
        // Arrange
        var datasource = databaseParameters.BuildDataSource();

        // Act
        var result = datasource.GetObservations().ToArray();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Length, Is.EqualTo(3));
        Assert.That(result[0].Types, Is.EquivalentTo(new DataType[] { DataType.Number, DataType.Text, DataType.Text, DataType.Date, DataType.Number, DataType.Text, DataType.Text, DataType.Numeric, DataType.Text, DataType.Number }));
        Assert.That(result[1].Types, Is.EquivalentTo(new DataType[] { DataType.Number, DataType.Text, DataType.Text, DataType.Date, DataType.Number, DataType.Text, DataType.Text, DataType.Numeric, DataType.Text, DataType.Number }));
        Assert.That(result[2].Types, Is.EquivalentTo(new DataType[] { DataType.Number, DataType.Text, DataType.Text, DataType.Date, DataType.Number, DataType.Text, DataType.Text, DataType.Numeric, DataType.Text, DataType.Number }));
    }
}
