namespace Hirundo.Databases.Tests.Integration;

[TestFixture]
public class CompositeDatabaseTests
{
    [Test]
    public void GivenTwoTablesInOneDatabase_WhenGetObservations_ReturnsBothTables()
    {
        // Arrange
        var oldDatabaseParameters = new AccessDatabaseParameters
        {
            Path = "./Assets/access_example_union_db.mdb",
            Table = "old_table",
            RingIdentifier = "RING",
            DateIdentifier = "DATE",
            SpeciesIdentifier = "SPECIES",
            Columns =
            [
                new ColumnParameters("IDR_Podab", "ID", DataValueType.LongInt),
                new ColumnParameters("RING", "RING", DataValueType.Text),
                new ColumnParameters("SPEC", "SPECIES", DataValueType.Text),
                new ColumnParameters("DATE", "DATE", DataValueType.DateTime),
                new ColumnParameters("MASS", "WEIGHT", DataValueType.Numeric)
            ]
        };

        var newDatabaseParameters = new AccessDatabaseParameters
        {
            Path = "./Assets/access_example_union_db.mdb",
            Table = "new_table",
            RingIdentifier = "RING",
            DateIdentifier = "DATE",
            SpeciesIdentifier = "SPECIES",
            Columns =
            [
                new ColumnParameters("IDR_Podab", "ID", DataValueType.LongInt),
                new ColumnParameters("RING", "RING", DataValueType.Text),
                new ColumnParameters("Species Code", "SPECIES", DataValueType.Text),
                new ColumnParameters("DATE2", "DATE", DataValueType.DateTime),
                new ColumnParameters("WEIGHT", "WEIGHT", DataValueType.Numeric)
            ]
        };

        var oldDatabase = new MdbAccessDatabase(oldDatabaseParameters);
        var newDatabase = new MdbAccessDatabase(newDatabaseParameters);

        var compositeDatabase = new CompositeDatabase(oldDatabase, newDatabase);

        // Act
        var data = compositeDatabase.GetObservations().ToList();

        // Assert
        Assert.That(data.Count, Is.EqualTo(5));

        Assert.That(data[0].GetValue("ID"), Is.EqualTo(1));
        Assert.That(data[0].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[0].GetValue("SPECIES"), Is.EqualTo("PHY.LUS"));
        Assert.That(data[0].GetValue("RING"), Is.EqualTo("J634038"));
        Assert.That(data[0].GetValue("DATE"), Is.EqualTo(new DateTime(1967, 08, 16)));
        Assert.That(data[0].GetValue("WEIGHT"), Is.EqualTo(9));

        Assert.That(data[1].GetValue("ID"), Is.EqualTo(2));
        Assert.That(data[1].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[1].GetValue("RING"), Is.EqualTo("J664040"));
        Assert.That(data[1].GetValue("SPECIES"), Is.EqualTo("PHY.LUS"));
        Assert.That(data[1].GetValue("DATE"), Is.EqualTo(new DateTime(1967, 08, 17)));
        Assert.That(data[1].GetValue("WEIGHT"), Is.EqualTo(9));

        Assert.That(data[2].GetValue("ID"), Is.EqualTo(1924534));
        Assert.That(data[2].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[2].GetValue("RING"), Is.EqualTo("LJ98545"));
        Assert.That(data[2].GetValue("SPECIES"), Is.EqualTo("REG.REG"));
        Assert.That(data[2].GetValue("DATE"), Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[2].GetValue("WEIGHT"), Is.EqualTo(4.9));

        Assert.That(data[3].GetValue("ID"), Is.EqualTo(1924535));
        Assert.That(data[3].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[3].GetValue("RING"), Is.EqualTo("LJ98546"));
        Assert.That(data[3].GetValue("SPECIES"), Is.EqualTo("CER.FAM"));
        Assert.That(data[3].GetValue("DATE"), Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[3].GetValue("WEIGHT"), Is.EqualTo(8.2));

        Assert.That(data[4].GetValue("ID"), Is.EqualTo(1924536));
        Assert.That(data[4].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[4].GetValue("RING"), Is.EqualTo("LJ98547"));
        Assert.That(data[4].GetValue("SPECIES"), Is.EqualTo("TRO.TRO"));
        Assert.That(data[4].GetValue("DATE"), Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[4].GetValue("WEIGHT"), Is.EqualTo(9.7));
    }
}