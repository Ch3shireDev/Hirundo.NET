using Hirundo.Databases.Serialization;
using Newtonsoft.Json.Linq;

namespace Hirundo.Databases.Tests;

[TestFixture]
public class DatabaseJsonSerializerTests
{
    [SetUp]
    public void Setup()
    {
        _serializer = new DatabaseJsonSerializer();
    }

    private DatabaseJsonSerializer _serializer = null!;

    [Test]
    public void GivenSqlServerParameters_WhenSerialize_ReturnsSqlServerParameters()
    {
        // Arrange
        var parameters = new SqlServerParameters
        {
            ConnectionString = "Server=localhost;Database=DB",
            Columns = [new ColumnMapping("SQL_COLUMN", "SQL_DATA", DataValueType.LongInt)]
        };

        // Act
        var json = _serializer.Serialize(parameters);

        // Assert
        var jobject = JObject.Parse(json);
        Assert.That(jobject["Type"], Is.Not.Null);
        Assert.That(jobject["Type"]?.ToString(), Is.EqualTo("SqlServer"));
        Assert.That(jobject["ConnectionString"]?.ToString(), Is.EqualTo("Server=localhost;Database=DB"));
        Assert.That(jobject["Columns"], Is.Not.Null);
        Assert.That(jobject["Columns"]?.Count(), Is.EqualTo(1));
        Assert.That(jobject["Columns"]?[0]?["DatabaseColumn"]?.ToString(), Is.EqualTo("SQL_COLUMN"));
        Assert.That(jobject["Columns"]?[0]?["ValueName"]?.ToString(), Is.EqualTo("SQL_DATA"));
        Assert.That(jobject["Columns"]?[0]?["DataType"]?.ToString(), Is.EqualTo("LongInt"));
    }

    [Test]
    public void GivenAccessParameters_WhenSerialize_ReturnsParameters()
    {
        // Arrange
        var parameters = new AccessDatabaseParameters
        {
            Path = "example.mdb",
            Table = "EXAMPLE_TABLE",
            Columns = [new ColumnMapping("COLUMN", "DATA", DataValueType.ShortInt)]
        };

        // Act
        var json = _serializer.Serialize(parameters);

        // Assert
        var jobject = JObject.Parse(json);
        Assert.That(jobject["Type"]?.ToString(), Is.EqualTo("Access"));
        Assert.That(jobject["Path"]?.ToString(), Is.EqualTo("example.mdb"));
        Assert.That(jobject["Table"]?.ToString(), Is.EqualTo("EXAMPLE_TABLE"));
        Assert.That(jobject["Columns"], Is.Not.Null);
        Assert.That(jobject["Columns"]?.Count(), Is.EqualTo(1));
        Assert.That(jobject["Columns"]?[0]?["DatabaseColumn"]?.ToString(), Is.EqualTo("COLUMN"));
        Assert.That(jobject["Columns"]?[0]?["ValueName"]?.ToString(), Is.EqualTo("DATA"));
        Assert.That(jobject["Columns"]?[0]?["DataType"]?.ToString(), Is.EqualTo("ShortInt"));
    }

    [Test]
    public void GivenAccessParameters_WhenDeserialize_ShouldReturnSameParameters()
    {
        // Arrange
        var parameters = new AccessDatabaseParameters
        {
            Path = "other_example.mdb",
            Table = "OTHER_EXAMPLE_TABLE",
            Columns = [new ColumnMapping("OTHER_COLUMN", "OTHER_DATA", DataValueType.String)]
        };

        // Act
        var json = _serializer.Serialize(parameters);
        var deserializedParameters = _serializer.Deserialize(json) as AccessDatabaseParameters;

        // Assert
        Assert.That(deserializedParameters, Is.Not.Null);
        Assert.That(deserializedParameters, Is.InstanceOf<AccessDatabaseParameters>());
        Assert.That(deserializedParameters?.Path, Is.EqualTo("other_example.mdb"));
        Assert.That(deserializedParameters?.Table, Is.EqualTo("OTHER_EXAMPLE_TABLE"));
        Assert.That(deserializedParameters?.Columns.Count, Is.EqualTo(1));
        Assert.That(deserializedParameters?.Columns[0].DatabaseColumn, Is.EqualTo("OTHER_COLUMN"));
        Assert.That(deserializedParameters?.Columns[0].ValueName, Is.EqualTo("OTHER_DATA"));
        Assert.That(deserializedParameters?.Columns[0].DataType, Is.EqualTo(DataValueType.String));
    }
}