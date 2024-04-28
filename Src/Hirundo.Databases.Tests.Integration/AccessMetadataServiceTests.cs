using Hirundo.Databases.Helpers;

namespace Hirundo.Databases.Tests.Integration;

[TestFixture]
public class AccessMetadataServiceTests
{
    [SetUp]
    public void Setup()
    {
        _service = new AccessMetadataService();
    }

    private AccessMetadataService _service = null!;

    [Test]
    public void GetTables_WhenLoadFromFile_ReturnsTablesList()
    {
        // Arrange
        var path = "./Assets/access_example_union_db.mdb";

        // Act
        var result = _service.GetTables(path).ToArray();

        // Assert
        Assert.That(result.Length, Is.EqualTo(2));

        var newTable = result.First(table => table.TableName == "new_table");
        Assert.That(newTable, Is.Not.Null);
        Assert.That(newTable.Columns.Count, Is.EqualTo(127));
        Assert.That(newTable.Columns[0].OrdinalPosition, Is.EqualTo(1));
        Assert.That(newTable.Columns[0].ColumnName, Is.EqualTo("IDR_Podab"));
        Assert.That(newTable.Columns[0].TypeName, Is.EqualTo("DOUBLE"));
        Assert.That(newTable.Columns[0].DataType, Is.EqualTo(8));
        Assert.That(newTable.Columns[0].IsNullable, Is.EqualTo(true));

        var oldTable = result.First(table => table.TableName == "old_table");
        Assert.That(oldTable, Is.Not.Null);
        Assert.That(oldTable.Columns.Count, Is.EqualTo(69));
        Assert.That(oldTable.Columns[0].OrdinalPosition, Is.EqualTo(1));
        Assert.That(oldTable.Columns[0].ColumnName, Is.EqualTo("IDR_Podab"));
        Assert.That(oldTable.Columns[0].TypeName, Is.EqualTo("COUNTER"));
        Assert.That(oldTable.Columns[0].DataType, Is.EqualTo(4));
        Assert.That(oldTable.Columns[0].IsNullable, Is.EqualTo(false));
    }

    [Test]
    public void GetDistinctValues_WhenLoadFromFileOldTable_ReturnsDistinctValues()
    {
        // Arrange
        var path = "./Assets/access_example_union_db.mdb";
        var tableName = "old_table";
        var columnName = "Spec";

        // Act
        var species = _service.GetDistinctValues(path, tableName, columnName).ToArray();

        // Assert
        Assert.That(species, Is.Not.Null);
        Assert.That(species.Length, Is.EqualTo(1));
        Assert.That(species[0], Is.EqualTo("PHY.LUS"));
    }

    [Test]
    public void GetDistinctValues_WhenLoadFromFileNewTable_ReturnsDistinctValues()
    {
        // Arrange
        var path = "./Assets/access_example_union_db.mdb";
        var tableName = "new_table";
        var columnName = "Species Code";

        // Act
        var species = _service.GetDistinctValues(path, tableName, columnName).ToArray();

        // Assert
        Assert.That(species, Is.Not.Null);
        Assert.That(species.Length, Is.EqualTo(3));
        Assert.That(species[0], Is.EqualTo("CER.FAM"));
        Assert.That(species[1], Is.EqualTo("REG.REG"));
        Assert.That(species[2], Is.EqualTo("TRO.TRO"));
    }
}