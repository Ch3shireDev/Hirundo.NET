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
    public void Test()
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

        var dataTypes = new Dictionary<int, string>();

        foreach (var column in oldTable.Columns)
        {
            dataTypes[column.DataType] = column.TypeName;
        }

        foreach (var column in newTable.Columns)
        {
            dataTypes[column.DataType] = column.TypeName;
        }

        foreach (var pair in dataTypes)
        {
            Console.WriteLine($"{pair.Key} => {pair.Value}");
        }
    }
}