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
        _builder.WithColumn(new ColumnMapping("RING", "RING", DataValueType.String));

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
    ///     Ze względu na błąd w starych bazach danych Access, nie mamy możliwości używania funkcji CDEC, która konwertuje
    ///     element na liczbę zmiennoprzecinkową.
    /// </summary>
    [Test]
    public void GivenDecimalColumn_WhenBuild_ReturnsQueryWithDoubleColumn()
    {
        // Arrange
        _builder.WithTable("example table 4");
        _builder.WithColumn(new ColumnMapping("WEIGHT", "WEIGHT", DataValueType.Decimal));

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
}