using Hirundo.Commons.Models;
using Hirundo.Databases.Conditions;

namespace Hirundo.Databases.Tests.Integration;

[TestFixture]
public class MdbAccessDatabaseTests
{
    [SetUp]
    public void Setup()
    {
        _accessDatabaseParameters = new AccessDatabaseParameters();
        _mdbAccessDatabase = new MdbAccessDatabase(_accessDatabaseParameters);
    }

    private AccessDatabaseParameters _accessDatabaseParameters = null!;
    private MdbAccessDatabase _mdbAccessDatabase = null!;

    [Test]
    public void GivenOldExampleDatabase_WhenGetData_ReturnsRows()
    {
        // Arrange
        _accessDatabaseParameters.Path = "./Assets/access_example_old_db.mdb";

        _accessDatabaseParameters.Columns.Add(new ColumnParameters("IDR_Podab", "ID", DataType.Number));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("RING", "RING", DataType.Text));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("DATE", "DATE", DataType.Date));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("MASS", "WEIGHT", DataType.Numeric));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("FAT", "FAT", DataType.Number));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("SPEC", "SPECIES", DataType.Text));

        _accessDatabaseParameters.Table = "TAB_RING_PODAB";

        _accessDatabaseParameters.RingIdentifier = "RING";
        _accessDatabaseParameters.DateIdentifier = "DATE";
        _accessDatabaseParameters.SpeciesIdentifier = "SPECIES";

        // Act
        var data = _mdbAccessDatabase.GetObservations().ToList();

        // Assert
        Assert.That(data.Count, Is.EqualTo(2));

        Assert.That(data[0].Ring, Is.EqualTo("J634038"));
        Assert.That(data[0].Date, Is.EqualTo(new DateTime(1967, 08, 16)));
        Assert.That(data[0].GetValue("ID"), Is.EqualTo(1));
        Assert.That(data[0].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[0].GetValue("RING"), Is.EqualTo("J634038"));
        Assert.That(data[0].GetValue("DATE"), Is.EqualTo(new DateTime(1967, 08, 16)));

        Assert.That(data[1].Ring, Is.EqualTo("J664040"));
        Assert.That(data[1].Date, Is.EqualTo(new DateTime(1967, 08, 17)));
        Assert.That(data[1].GetValue("ID"), Is.EqualTo(2));
        Assert.That(data[1].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[1].GetValue("RING"), Is.EqualTo("J664040"));
        Assert.That(data[1].GetValue("DATE"), Is.EqualTo(new DateTime(1967, 08, 17)));
    }

    [Test]
    public void GivenNewExampleDatabse_WhenGetData_ReturnsThreeRows()
    {
        // Arrange
        _accessDatabaseParameters.Path = "./Assets/access_example_new_db.mdb";
        _accessDatabaseParameters.Table = "example table";

        _accessDatabaseParameters.RingIdentifier = "RING";
        _accessDatabaseParameters.DateIdentifier = "DATE";
        _accessDatabaseParameters.SpeciesIdentifier = "SPECIES";

        _accessDatabaseParameters.Columns.Add(new ColumnParameters("IDR_Podab", "ID", DataType.Number));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("RING", "RING", DataType.Text));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("Date2", "DATE", DataType.Date));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("Species Code", "SPECIES", DataType.Text));

        // Act
        var data = _mdbAccessDatabase.GetObservations().ToList();

        // Assert
        Assert.That(data.Count, Is.EqualTo(3));

        Assert.That(data[0].Ring, Is.EqualTo("LJ98545"));
        Assert.That(data[0].Date, Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[0].GetValue("ID"), Is.EqualTo(1924534));
        Assert.That(data[0].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[0].GetValue("RING"), Is.EqualTo("LJ98545"));
        Assert.That(data[0].GetValue("DATE"), Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[0].GetValue("SPECIES"), Is.EqualTo("REG.REG"));

        Assert.That(data[1].Ring, Is.EqualTo("LJ98546"));
        Assert.That(data[1].Date, Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[1].GetValue("ID"), Is.EqualTo(1924535));
        Assert.That(data[1].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[1].GetValue("RING"), Is.EqualTo("LJ98546"));
        Assert.That(data[1].GetValue("DATE"), Is.EqualTo(new DateTime(2017, 03, 26)));

        Assert.That(data[2].Ring, Is.EqualTo("LJ98547"));
        Assert.That(data[2].Date, Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[2].GetValue("ID"), Is.EqualTo(1924536));
        Assert.That(data[2].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[2].GetValue("RING"), Is.EqualTo("LJ98547"));
        Assert.That(data[2].GetValue("DATE"), Is.EqualTo(new DateTime(2017, 03, 26)));
    }

    [Test]
    public void GivenNewExampleDatabase_WhenGetData_ReadsTypesCorrectly()
    {
        // Arrange
        _accessDatabaseParameters.Path = "./Assets/access_example_new_db.mdb";
        _accessDatabaseParameters.Table = "example table";

        _accessDatabaseParameters.RingIdentifier = "RING";
        _accessDatabaseParameters.DateIdentifier = "DATE";
        _accessDatabaseParameters.SpeciesIdentifier = "SPECIES";

        _accessDatabaseParameters.Columns.Add(new ColumnParameters("IDR_Podab", "ID", DataType.Number));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("RING", "RING", DataType.Text));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("Date2", "DATE", DataType.Date));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("Species Code", "SPECIES", DataType.Text));

        // Act
        var data = _mdbAccessDatabase.GetObservations().ToList();

        // Assert
        Assert.That(data.Count, Is.EqualTo(3));
        Assert.That(data[0].Types, Is.EquivalentTo(new[] { DataType.Number, DataType.Text, DataType.Date, DataType.Text }));
        Assert.That(data[1].Types, Is.EquivalentTo(new[] { DataType.Number, DataType.Text, DataType.Date, DataType.Text }));
        Assert.That(data[2].Types, Is.EquivalentTo(new[] { DataType.Number, DataType.Text, DataType.Date, DataType.Text }));
    }

    [Test]
    public void GivenNewExampleDatabaseWithSpeciesClause_WhenGetData_ReturnsOnlyRegReg()
    {
        // Arrange
        _accessDatabaseParameters.Path = "./Assets/access_example_new_db.mdb";
        _accessDatabaseParameters.Table = "example table";

        _accessDatabaseParameters.RingIdentifier = "RING";
        _accessDatabaseParameters.DateIdentifier = "DATE";
        _accessDatabaseParameters.SpeciesIdentifier = "SPECIES";

        _accessDatabaseParameters.Columns.Add(new ColumnParameters("IDR_Podab", "ID", DataType.Number));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("RING", "RING", DataType.Text));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("DATE2", "DATE", DataType.Date));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("Species Code", "SPECIES", DataType.Text));

        _accessDatabaseParameters.Conditions.Add(new DatabaseCondition("Species Code", "REG.REG", DatabaseConditionType.IsEqual));

        // Act
        var data = _mdbAccessDatabase.GetObservations().ToList();

        // Assert
        Assert.That(data.Count, Is.EqualTo(1));
        Assert.That(data[0].Ring, Is.EqualTo("LJ98545"));
        Assert.That(data[0].Date, Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[0].GetValue("ID"), Is.EqualTo(1924534));
        Assert.That(data[0].GetValue("ID"), Is.InstanceOf<int>());
        Assert.That(data[0].GetValue("RING"), Is.EqualTo("LJ98545"));
        Assert.That(data[0].GetValue("SPECIES"), Is.EqualTo("REG.REG"));
    }

    [Test]
    public void GivenWhereClauseForIntValue_WhenGetData_ReturnsValueWithThisIntValue()
    {
        // Arrange
        _accessDatabaseParameters.Path = "./Assets/access_example_new_db.mdb";
        _accessDatabaseParameters.Table = "example table";

        _accessDatabaseParameters.RingIdentifier = "RING";
        _accessDatabaseParameters.DateIdentifier = "DATE";
        _accessDatabaseParameters.SpeciesIdentifier = "SPECIES";

        _accessDatabaseParameters.Columns.Add(new ColumnParameters("IDR_Podab", "ID", DataType.Number));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("RING", "RING", DataType.Text));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("DATE2", "DATE", DataType.Date));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("Species Code", "SPECIES", DataType.Text));

        _accessDatabaseParameters.Conditions.Add(new DatabaseCondition("IDR_Podab", 1924534, DatabaseConditionType.IsEqual));

        // Act
        var data = _mdbAccessDatabase.GetObservations().ToList();

        // Assert
        Assert.That(data.Count, Is.EqualTo(1));
        Assert.That(data[0].GetValue("ID"), Is.EqualTo(1924534));
        Assert.That(data[0].Ring, Is.EqualTo("LJ98545"));
        Assert.That(data[0].Date, Is.EqualTo(new DateTime(2017, 03, 26)));
    }

    [Test]
    public void GivenWhereClauseForDate_WhenGetData_ReturnsProperDateValue()
    {
        // Arrange
        _accessDatabaseParameters.Path = "./Assets/access_example_new_db.mdb";
        _accessDatabaseParameters.Table = "example table";

        _accessDatabaseParameters.RingIdentifier = "RING";
        _accessDatabaseParameters.DateIdentifier = "DATE";
        _accessDatabaseParameters.SpeciesIdentifier = "SPECIES";

        _accessDatabaseParameters.Columns.Add(new ColumnParameters("IDR_Podab", "ID", DataType.Number));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("RING", "RING", DataType.Text));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("Date2", "DATE", DataType.Date));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("Species Code", "SPECIES", DataType.Text));

        _accessDatabaseParameters.Conditions.Add(new DatabaseCondition("DATE2", "2017-03-26", DatabaseConditionType.IsEqual));

        // Act
        var data = _mdbAccessDatabase.GetObservations().ToList();

        // Assert
        Assert.That(data.Count, Is.EqualTo(3));

        Assert.That(data[0].Ring, Is.EqualTo("LJ98545"));
        Assert.That(data[0].Date, Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[0].GetValue("ID"), Is.EqualTo(1924534));

        Assert.That(data[1].Ring, Is.EqualTo("LJ98546"));
        Assert.That(data[1].Date, Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[1].GetValue("ID"), Is.EqualTo(1924535));

        Assert.That(data[2].Ring, Is.EqualTo("LJ98547"));
        Assert.That(data[2].Date, Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[2].GetValue("ID"), Is.EqualTo(1924536));
    }

    [Test]
    public void GivenTwoWhereClausesForDate_WhenGetData_ReturnsProperDateValue()
    {
        // Arrange
        _accessDatabaseParameters.Path = "./Assets/access_example_new_db.mdb";
        _accessDatabaseParameters.Table = "example table";

        _accessDatabaseParameters.RingIdentifier = "RING";
        _accessDatabaseParameters.DateIdentifier = "DATE";
        _accessDatabaseParameters.SpeciesIdentifier = "SPECIES";

        _accessDatabaseParameters.Columns.Add(new ColumnParameters("IDR_Podab", "ID", DataType.Number));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("RING", "RING", DataType.Text));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("DATE2", "DATE", DataType.Date));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("Species Code", "SPECIES", DataType.Text));

        _accessDatabaseParameters.Conditions.Add(new DatabaseCondition("DATE2", "2017-03-25", DatabaseConditionType.IsGreaterThan));
        _accessDatabaseParameters.Conditions.Add(new DatabaseCondition("DATE2", "2017-03-27", DatabaseConditionType.IsLowerThan));

        // Act
        var data = _mdbAccessDatabase.GetObservations().ToList();

        // Assert
        Assert.That(data.Count, Is.EqualTo(3));

        Assert.That(data[0].Ring, Is.EqualTo("LJ98545"));
        Assert.That(data[0].Date, Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[0].GetValue("ID"), Is.EqualTo(1924534));

        Assert.That(data[1].Ring, Is.EqualTo("LJ98546"));
        Assert.That(data[1].Date, Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[1].GetValue("ID"), Is.EqualTo(1924535));

        Assert.That(data[2].Ring, Is.EqualTo("LJ98547"));
        Assert.That(data[2].Date, Is.EqualTo(new DateTime(2017, 03, 26)));
        Assert.That(data[2].GetValue("ID"), Is.EqualTo(1924536));
    }

    [Test]
    public void GivenTwoWhereClausesForDateInOldDatabase_WhenGetData_ReturnsProperDateValue()
    {
        // Arrange
        _accessDatabaseParameters.Path = "./Assets/access_example_old_db.mdb";
        _accessDatabaseParameters.Table = "TAB_RING_PODAB";
        _accessDatabaseParameters.RingIdentifier = "RING";
        _accessDatabaseParameters.DateIdentifier = "DATE";
        _accessDatabaseParameters.SpeciesIdentifier = "SPECIES";

        _accessDatabaseParameters.Columns.Add(new ColumnParameters("IDR_Podab", "ID", DataType.Number));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("RING", "RING", DataType.Text));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("SPEC", "SPECIES", DataType.Text));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("DATE", "DATE", DataType.Date));

        _accessDatabaseParameters.Conditions.Add(new DatabaseCondition("DATE", "1967-08-15", DatabaseConditionType.IsGreaterThan));
        _accessDatabaseParameters.Conditions.Add(new DatabaseCondition("DATE", "1967-08-17", DatabaseConditionType.IsLowerThan));

        // Act
        var data = _mdbAccessDatabase.GetObservations().ToList();

        // Assert
        Assert.That(data.Count, Is.EqualTo(1));
        Assert.That(data[0].GetValue("ID"), Is.EqualTo(1));
    }

    [Test]
    public void GivenDateClauseThatIsNotPossible_WhenGetData_ReturnsNoValue()
    {
        // Arrange
        _accessDatabaseParameters.Path = "./Assets/access_example_new_db.mdb";
        _accessDatabaseParameters.Table = "example table";
        _accessDatabaseParameters.RingIdentifier = "RING";
        _accessDatabaseParameters.DateIdentifier = "DATE";
        _accessDatabaseParameters.SpeciesIdentifier = "SPECIES";
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("IDR_Podab", "ID", DataType.Number));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("RING", "RING", DataType.Text));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("Species Code", "SPECIES", DataType.Text));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("DATE2", "DATE", DataType.Date));
        _accessDatabaseParameters.Conditions.Add(new DatabaseCondition("DATE2", "2017-03-25", DatabaseConditionType.IsLowerThan));

        // Act
        var data = _mdbAccessDatabase.GetObservations().ToList();

        // Assert
        Assert.That(data.Count, Is.EqualTo(0));
    }

    [Test]
    public void GivenSpeciesConfig_WhenGetData_ReturnsObservationsWithSpeciesFilled()
    {
        _accessDatabaseParameters.Path = "./Assets/access_example_new_db.mdb";
        _accessDatabaseParameters.Table = "example table";
        _accessDatabaseParameters.RingIdentifier = "RING";
        _accessDatabaseParameters.DateIdentifier = "DATE";
        _accessDatabaseParameters.SpeciesIdentifier = "SPECIES";
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("IDR_Podab", "ID", DataType.Number));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("RING", "RING", DataType.Text));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("Species Code", "SPECIES", DataType.Text));
        _accessDatabaseParameters.Columns.Add(new ColumnParameters("DATE2", "DATE", DataType.Date));

        // Act
        var data = _mdbAccessDatabase.GetObservations().ToList();

        // Assert
        Assert.That(data.Count, Is.EqualTo(3));
        Assert.That(data[0].Species, Is.EqualTo("REG.REG"));
        Assert.That(data[1].Species, Is.EqualTo("CER.FAM"));
        Assert.That(data[2].Species, Is.EqualTo("TRO.TRO"));
    }
}