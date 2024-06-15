using Hirundo.Commons.Models;
using Hirundo.Databases.Conditions;

namespace Hirundo.Databases.Tests;

[TestFixture]
public class DatabaseBuilderTests
{
    [SetUp]
    public void Initialize()
    {
        _builder = new DatabaseBuilder();
    }

    private DatabaseBuilder _builder = null!;

    [Test]
    public void GivenDatabaseParameters_WhenAddMdbAccessDatabase_ReturnsDatabaseBuilder()
    {
        // Arrange
        var parameters = new AccessDatabaseParameters
        {
            Path = "example path",
            Table = "example table",
            Columns =
            [
                new ColumnParameters("IDR_Podab", "ID", DataType.Number),
                new ColumnParameters("RING", "RING", DataType.Text)
            ],
            Conditions =
            [
                new DatabaseCondition("ID", "1", DatabaseConditionType.IsEqual)
            ]
        };

        // Act
        var result = _builder.WithDatabaseParameters([parameters]).Build();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<MdbAccessDatabase>());
        var accessDatabase = result as MdbAccessDatabase;
        Assert.That(accessDatabase, Is.Not.Null);
        Assert.That(accessDatabase?.CancellationToken, Is.Null);
        Assert.That(accessDatabase?.Parameters, Is.EqualTo(parameters));
    }

    [Test]
    public void GivenDatabaseParametersAndCancellationToken_WhenAddCancellationToken_ReturnsDatabaseBuilderWithCancellationToken()
    {
        // Arrange
        var parameters = new AccessDatabaseParameters
        {
            Path = "example path",
            Table = "example table",
            Columns =
            [
                new ColumnParameters("IDR_Podab", "ID", DataType.Number),
                new ColumnParameters("RING", "RING", DataType.Text)
            ],
            Conditions =
            [
                new DatabaseCondition("ID", "1", DatabaseConditionType.IsEqual)
            ]
        };

        var cancellationToken = new CancellationToken();

        // Act
        var result = _builder.WithDatabaseParameters([parameters]).WithCancellationToken(cancellationToken).Build();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<MdbAccessDatabase>());
        var accessDatabase = result as MdbAccessDatabase;
        Assert.That(accessDatabase, Is.Not.Null);
        Assert.That(accessDatabase?.CancellationToken, Is.EqualTo(cancellationToken));
        Assert.That(accessDatabase?.Parameters, Is.EqualTo(parameters));
    }
}