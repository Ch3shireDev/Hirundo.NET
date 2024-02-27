using Hirundo.Databases.Conditions;

namespace Hirundo.Databases.Tests;

[TestFixture]
public class MdbAccessQueryBuilderTests
{
    [SetUp]
    public void Setup()
    {
        _builder = new MdbAccessQueryBuilder();
    }

    private MdbAccessQueryBuilder _builder = null!;

    [Test]
    public void GivenSingleLongIntColumn_WhenBuild_ReturnsQueryWithSingleIntColumn()
    {
        // Arrange
        _builder.WithTable("example table");
        _builder.WithColumn(new ColumnMapping("IDR_Podab", "ID", DataValueType.LongInt));

        // Act
        var query = _builder.Build();

        // Assert
        Assert.That(query, Is.EqualTo("SELECT IIF(IsNull([IDR_Podab]), Null, CLNG([IDR_Podab])) FROM [example table]"));
    }

    [Test]
    public void GivenTwoColumns_WhenBuild_ReturnsQueryForTwoColumns()
    {
        // Arrange
        _builder.WithTable("example table 2");
        _builder.WithColumn(new ColumnMapping("IDR_Podab", "ID", DataValueType.LongInt));
        _builder.WithColumn(new ColumnMapping("RING", "RING", DataValueType.Text));

        // Act
        var query = _builder.Build();

        // Assert
        Assert.That(query, Is.EqualTo("SELECT IIF(IsNull([IDR_Podab]), Null, CLNG([IDR_Podab])), IIF(IsNull([RING]), Null, CSTR([RING])) FROM [example table 2]"));
    }

    [Test]
    public void GivenShortIntColumn_WhenBuild_ReturnsQueryWithShortIntColumn()
    {
        // Arrange
        _builder.WithTable("example table 3");
        _builder.WithColumn(new ColumnMapping("HOUR", "HOUR", DataValueType.ShortInt));

        // Act
        var query = _builder.Build();

        // Assert
        Assert.That(query, Is.EqualTo("SELECT IIF(IsNull([HOUR]), Null, CINT([HOUR])) FROM [example table 3]"));
    }

    /// <summary>
    ///     Ze względu na błąd w starych bazach danych Access, nie mamy możliwości używania funkcji CDEC,
    ///     która konwertuje element na liczbę zmiennoprzecinkową.
    /// </summary>
    [Test]
    public void GivenDecimalColumn_WhenBuild_ReturnsQueryWithDoubleColumn()
    {
        // Arrange
        _builder.WithTable("example table 4");
        _builder.WithColumn(new ColumnMapping("WEIGHT", "WEIGHT", DataValueType.Numeric));

        // Act
        var query = _builder.Build();

        // Assert
        Assert.That(query, Is.EqualTo("SELECT IIF(IsNull([WEIGHT]), Null, CDBL([WEIGHT])) FROM [example table 4]"));
    }

    [Test]
    public void GivenDateTimeColumn_WhenBuild_ReturnsQueryWithDateTimeColumn()
    {
        // Arrange
        _builder.WithTable("example table 5");
        _builder.WithColumn(new ColumnMapping("DATE", "DATE", DataValueType.DateTime));

        // Act
        var query = _builder.Build();

        // Assert
        Assert.That(query, Is.EqualTo("SELECT IIF(IsNull([DATE]), Null, CDATE([DATE])) FROM [example table 5]"));
    }

    [Test]
    public void GivenDatabaseCondition_WhenBuild_TranslatesToWhereClause()
    {
        // Arrange
        _builder.WithTable("example table 6");
        _builder.WithColumn(new ColumnMapping("IDR_Podab", "ID", DataValueType.LongInt));
        _builder.WithCondition(new DatabaseCondition("SPECIES", "REG.REG", DatabaseConditionType.IsEqual));

        // Act
        var query = _builder.Build();

        // Assert
        Assert.That(query, Is.EqualTo("SELECT IIF(IsNull([IDR_Podab]), Null, CLNG([IDR_Podab])) FROM [example table 6] WHERE [SPECIES] = 'REG.REG'"));
    }

    [Test]
    public void GivenTwoDatabaseConditions_WhenBuild_TranslatesToWhereClause()
    {
        // Arrange
        _builder.WithTable("example table 7");
        _builder.WithColumn(new ColumnMapping("IDR_Podab", "ID", DataValueType.LongInt));
        _builder.WithCondition(new DatabaseCondition("SPECIES", "REG.REG", DatabaseConditionType.IsEqual));
        _builder.WithCondition(new DatabaseCondition("RING", "J634038", DatabaseConditionType.IsEqual));

        // Act
        var query = _builder.Build();

        // Assert
        Assert.That(query, Is.EqualTo("SELECT IIF(IsNull([IDR_Podab]), Null, CLNG([IDR_Podab])) FROM [example table 7] WHERE [SPECIES] = 'REG.REG' AND [RING] = 'J634038'"));
    }

    [Test]
    public void GivenTwoDatabaseConditions_WhenBuild_TranslatesToWhereClauseWithOr()
    {
        // Arrange
        _builder.WithTable("example table 8");
        _builder.WithColumn(new ColumnMapping("IDR_Podab", "ID", DataValueType.LongInt));
        _builder.WithCondition(new DatabaseCondition("SPECIES", "REG.REG", DatabaseConditionType.IsEqual));
        _builder.WithCondition(new DatabaseCondition("RING", "J634038", DatabaseConditionType.IsEqual, DatabaseConditionOperator.Or));

        // Act
        var query = _builder.Build();

        // Assert
        Assert.That(query, Is.EqualTo("SELECT IIF(IsNull([IDR_Podab]), Null, CLNG([IDR_Podab])) FROM [example table 8] WHERE [SPECIES] = 'REG.REG' OR [RING] = 'J634038'"));
    }

    [Test]
    public void GivenThreeDatabaseConditions_WhenBuild_TranslatesToWhereClauseWithAndOr()
    {
        // Arrange
        _builder.WithTable("example table 9");
        _builder.WithColumn(new ColumnMapping("IDR_Podab", "ID", DataValueType.LongInt));
        _builder.WithCondition(new DatabaseCondition("SPECIES", "REG.REG", DatabaseConditionType.IsEqual));
        _builder.WithCondition(new DatabaseCondition("RING", "J634038", DatabaseConditionType.IsEqual, DatabaseConditionOperator.Or));
        _builder.WithCondition(new DatabaseCondition("RING", "J664040", DatabaseConditionType.IsEqual));

        // Act
        var query = _builder.Build();

        // Assert
        Assert.That(query, Is.EqualTo("SELECT IIF(IsNull([IDR_Podab]), Null, CLNG([IDR_Podab])) FROM [example table 9] WHERE [SPECIES] = 'REG.REG' OR [RING] = 'J634038' AND [RING] = 'J664040'"));
    }

    [Test]
    public void GivenConditionsWithDates_WhenBuild_TranslatesToTwoDateConditions()
    {
        // Arrange
        _builder.WithTable("example table 10");
        _builder.WithColumn(new ColumnMapping("IDR_Podab", "ID", DataValueType.LongInt));
        _builder.WithCondition(new DatabaseCondition("DATE", new DateTime(1967, 08, 16), DatabaseConditionType.IsGreaterThan));
        _builder.WithCondition(new DatabaseCondition("DATE", new DateTime(1967, 08, 17), DatabaseConditionType.IsLowerThan));

        // Act
        var query = _builder.Build();

        // Assert
        Assert.That(query, Is.EqualTo("SELECT IIF(IsNull([IDR_Podab]), Null, CLNG([IDR_Podab])) FROM [example table 10] WHERE [DATE] > DateSerial(1967, 8, 16) AND [DATE] < DateSerial(1967, 8, 17)"));
    }

    [Test]
    public void GivenNotEqualOperator_WhenBuild_TranslatesToInequalityOperator()
    {
        // Arrange
        _builder.WithTable("example table 11");
        _builder.WithColumn(new ColumnMapping("IDR_Podab", "ID", DataValueType.LongInt));
        _builder.WithCondition(new DatabaseCondition("SPECIES", "REG.REG", DatabaseConditionType.IsNotEqual));

        // Act
        var query = _builder.Build();

        // Assert
        Assert.That(query, Is.EqualTo("SELECT IIF(IsNull([IDR_Podab]), Null, CLNG([IDR_Podab])) FROM [example table 11] WHERE [SPECIES] <> 'REG.REG'"));
    }
}