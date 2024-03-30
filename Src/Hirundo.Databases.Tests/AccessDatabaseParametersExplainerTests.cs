namespace Hirundo.Databases.Tests;

[TestFixture]
public class AccessDatabaseParametersExplainerTests
{
    private AccessDatabaseParametersExplainer _explainer = null!;
    private AccessDatabaseParameters _params = null!;
    [SetUp]
    public void Initialize()
    {
        _params = new AccessDatabaseParameters();
        _explainer = new AccessDatabaseParametersExplainer();
    }

    [Test]
    public void GivenEmptyConfig_WhenExplaining_ThenReturnSimpleText()
    {
        // Arrange
        _params.Path = "C:\\data\\test.mdb";
        _params.Table = "TestTable";
        _explainer.DataSourceInfo = "Pobiera dane z pliku {0}, z tabeli {1}.";
        _explainer.ColumnInfo = "Zapisuje kolumnę {0} jako {1}, typu {2}.";

        var column = new ColumnParameters { DatabaseColumn = "ID", ValueName = "Id", DataType = DataValueType.LongInt };
        _params.Columns.Add(column);

        // Act
        var explanation = _explainer.Explain(_params);

        // Assert
        Assert.That(explanation, Does.Contain("Pobiera dane z pliku C:\\data\\test.mdb, z tabeli TestTable."));
        Assert.That(explanation, Does.Contain("Zapisuje kolumnę ID jako Id, typu duża liczba całkowita."));
    }
}