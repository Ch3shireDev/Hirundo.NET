using Hirundo.Commons.Models;
using Hirundo.Databases;
using NUnit.Framework;

namespace Hirundo.App.Tests.Integration;

[TestFixture]
public class AccessDatabaseParametersExplainerTests
{
    [SetUp]
    public void Initialize()
    {
        _params = new AccessDatabaseParameters();
    }

    private AccessDatabaseParameters _params = null!;

    [Test]
    public void GivenEmptyConfig_WhenExplaining_ThenReturnSimpleText()
    {
        // Arrange
        _params.Path = "C:\\data\\test.mdb";
        _params.Table = "TestTable";
        //_explainer.DataSourceInfo = "Pobiera dane z pliku {0}, z tabeli {1}.";
        //_explainer.ColumnInfo = "Zapisuje kolumnę {0} jako {1}, typu {2}.";

        var column = new ColumnParameters { DatabaseColumn = "ID", ValueName = "Id", DataType = DataType.Number };
        _params.Columns.Add(column);

        // Act
        var explanation = _params.Explain();

        // Assert
        Assert.That(explanation, Does.Contain("Pobiera dane z pliku C:\\data\\test.mdb, z tabeli TestTable."));
        Assert.That(explanation, Does.Contain("Zapisuje kolumnę ID jako Id, typu liczba całkowita."));
    }
}