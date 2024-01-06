using System.Text;

namespace Hirundo.Databases;

/// <summary>
///     Budowniczy zapytań dla bazy danych Access 2003 (pliki .mdb). Zapytanie jest tworzone na podstawie nazwy tabeli oraz
///     <see cref="DatabaseColumn" />.
/// </summary>
public class MdbAccessQueryBuilder
{
    private readonly List<DatabaseColumn> _columns = [];
    private string? _tableName;

    public MdbAccessQueryBuilder WithTable(string tableName)
    {
        _tableName = tableName;
        return this;
    }

    public MdbAccessQueryBuilder WithColumns(IEnumerable<DatabaseColumn> columns)
    {
        _columns.AddRange(columns);
        return this;
    }

    public MdbAccessQueryBuilder WithColumn(DatabaseColumn column)
    {
        _columns.Add(column);
        return this;
    }

    /// <summary>
    ///     Przygotowywana jest kwerenda SQL na podstawie parametrów wejściowych.
    /// </summary>
    /// <returns></returns>
    public string Build()
    {
        var stringBuider = new StringBuilder();
        stringBuider.Append("SELECT ");
        stringBuider.Append(string.Join(", ", _columns.Select(GetSqlColumnExpression)));
        stringBuider.Append($" FROM [{_tableName}]");
        return stringBuider.ToString();
    }

    /// <summary>
    ///     Metoda odpowiedzialna za konwersję danych na poziomie zapytania SQL. Używane funkcje sprawiają, że
    ///     dane są przetwarzane około 3x dłużej (z 40s to 100s).
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private string GetSqlColumnExpression(DatabaseColumn column)
    {
        return column.DataValueType switch
        {
            DataValueType.Undefined => $"[{column.SqlColumnName}]",
            DataValueType.String => $"IIF(IsNull([{column.SqlColumnName}]), Null, CSTR([{column.SqlColumnName}]))",
            DataValueType.ShortInt => $"IIF(IsNull([{column.SqlColumnName}]), Null, CINT([{column.SqlColumnName}]))",
            DataValueType.LongInt => $"IIF(IsNull([{column.SqlColumnName}]), Null, CLNG([{column.SqlColumnName}]))",
            DataValueType.Decimal => $"IIF(IsNull([{column.SqlColumnName}]), Null, CDBL([{column.SqlColumnName}]))",
            DataValueType.DateTime => $"IIF(IsNull([{column.SqlColumnName}]), Null, CDATE([{column.SqlColumnName}]))",
            _ => throw new ArgumentOutOfRangeException(nameof(column), column.DataValueType, null)
        };
    }
}