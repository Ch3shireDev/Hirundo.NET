namespace Hirundo.Databases.Tests.Integration;

public class MdbAccessDatabaseTests
{
    private AccessDatabaseParameters _accessDatabaseParameters = null!;
    private MdbAccessDatabase _mdbAccessDatabase = null!;

    [SetUp]
    public void Setup()
    {
        _accessDatabaseParameters = new AccessDatabaseParameters();
        _mdbAccessDatabase = new MdbAccessDatabase(_accessDatabaseParameters);
    }

    [Test]
    public void GivenOldExampleDatabase_WhenGetData_ReturnsRows()
    {
        // Arrange
        _accessDatabaseParameters.FilePath = "./Assets/access_example_old_db.mdb";
        _accessDatabaseParameters.ValuesColumns.Add(new DatabaseColumn("IDR_Podab", "ID", DataValueType.LongInt));
        _accessDatabaseParameters.ValuesColumns.Add(new DatabaseColumn("RING", "RING", DataValueType.String));
        _accessDatabaseParameters.ValuesColumns.Add(new DatabaseColumn("DATE", "DATE", DataValueType.DateTime));
        _accessDatabaseParameters.ValuesColumns.Add(new DatabaseColumn("MASS", "WEIGHT", DataValueType.Decimal));
        _accessDatabaseParameters.ValuesColumns.Add(new DatabaseColumn("FAT", "FAT", DataValueType.ShortInt));
        _accessDatabaseParameters.TableName = "TAB_RING_PODAB";

        // Act
        var data = _mdbAccessDatabase.GetObservations().ToList();

        // Assert
        Assert.That(data.Count, Is.EqualTo(2));
        Assert.That(data[0].GetValue("ID"), Is.EqualTo(1));
        Assert.That(data[0].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[0].GetValue("RING"), Is.EqualTo("J634038"));
        Assert.That(data[0].GetValue("DATE"), Is.EqualTo(new DateTime(1967, 08, 16)));
        Assert.That(data[1].GetValue("ID"), Is.EqualTo(2));
        Assert.That(data[1].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[1].GetValue("RING"), Is.EqualTo("J664040"));
        Assert.That(data[1].GetValue("DATE"), Is.EqualTo(new DateTime(1967, 08, 17)));
    }

    [Test]
    public void GivenNewExampleDatabse_WhenGetData_ReturnsThreeRows()
    {
        // Arrange
        _accessDatabaseParameters.FilePath = "./Assets/access_example_new_db.mdb";
        _accessDatabaseParameters.ValuesColumns.Add(new DatabaseColumn("IDR_Podab", "ID", DataValueType.LongInt));
        _accessDatabaseParameters.ValuesColumns.Add(new DatabaseColumn("RING", "RING", DataValueType.String));
        _accessDatabaseParameters.ValuesColumns.Add(new DatabaseColumn("Date2", "DATE", DataValueType.DateTime));
        _accessDatabaseParameters.ValuesColumns.Add(new DatabaseColumn("Species Code", "SPECIES", DataValueType.String));
        _accessDatabaseParameters.TableName = "example table";

        // Act
        var data = _mdbAccessDatabase.GetObservations().ToList();

        // Assert
        Assert.That(data.Count, Is.EqualTo(3));
        Assert.That(data[0].GetValue("ID"), Is.EqualTo(1924534));
        Assert.That(data[0].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[0].GetValue("RING"), Is.EqualTo("LJ98545"));
        Assert.That(data[0].GetValue("DATE"), Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[1].GetValue("ID"), Is.EqualTo(1924535));
        Assert.That(data[1].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[1].GetValue("RING"), Is.EqualTo("LJ98546"));
        Assert.That(data[1].GetValue("DATE"), Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[2].GetValue("ID"), Is.EqualTo(1924536));
        Assert.That(data[2].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[2].GetValue("RING"), Is.EqualTo("LJ98547"));
        Assert.That(data[2].GetValue("DATE"), Is.EqualTo(new DateTime(2017, 03, 26)));
    }
}