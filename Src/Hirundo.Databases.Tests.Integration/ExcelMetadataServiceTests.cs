using Hirundo.Databases.Helpers;

namespace Hirundo.Databases.Tests.Integration;

[TestFixture]
public class ExcelMetadataServiceTests
{
    private ExcelMetadataService _service = null!;

    [SetUp]
    public void Initialize()
    {
        _service = new ExcelMetadataService();
    }

    [Test]
    public void GetColumn_ReturnsProperValues()
    {
        // Arrange
        var path = "./Assets/example-data.xlsx";

        // Act
        var columns = _service.GetColumns(path).ToArray();

        // Assert
        Assert.That(columns.Length, Is.EqualTo(10));
        var columnNames = new[] { "ID", "RING", "SPECIES", "DATE", "HOUR", "SEX", "AGE", "WEIGHT", "STATUS", "FAT" };
        Assert.That(columns.Select(c => c.DatabaseColumn), Is.EquivalentTo(columnNames));
        Assert.That(columns.Select(c => c.ValueName), Is.EquivalentTo(columnNames));
    }
}
