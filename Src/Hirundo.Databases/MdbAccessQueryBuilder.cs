using System.Text;

namespace Hirundo.Databases;

/// <summary>
///     Budowniczy zapytań dla bazy danych Access 2003 (pliki .mdb). Zapytanie jest tworzone na podstawie nazwy tabeli oraz
///     <see cref="ColumnMapping" />.
/// </summary>
public class MdbAccessQueryBuilder
{
    private readonly List<ColumnMapping> _columns = [];
    private string? _tableName;

    public MdbAccessQueryBuilder WithTable(string tableName)
    {
        _tableName = tableName;
        return this;
    }

    public MdbAccessQueryBuilder WithColumns(IEnumerable<ColumnMapping> columns)
    {
        _columns.AddRange(columns);
        return this;
    }

    public MdbAccessQueryBuilder WithColumn(ColumnMapping columnMapping)
    {
        _columns.Add(columnMapping);
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
    /// <param name="columnMapping"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private string GetSqlColumnExpression(ColumnMapping columnMapping)
    {
        return columnMapping.DataType switch
        {
            DataValueType.Undefined => $"[{columnMapping.DatabaseColumn}]",
            DataValueType.String => $"IIF(IsNull([{columnMapping.DatabaseColumn}]), Null, CSTR([{columnMapping.DatabaseColumn}]))",
            DataValueType.ShortInt => $"IIF(IsNull([{columnMapping.DatabaseColumn}]), Null, CINT([{columnMapping.DatabaseColumn}]))",
            DataValueType.LongInt => $"IIF(IsNull([{columnMapping.DatabaseColumn}]), Null, CLNG([{columnMapping.DatabaseColumn}]))",
            DataValueType.Decimal => $"IIF(IsNull([{columnMapping.DatabaseColumn}]), Null, CDBL([{columnMapping.DatabaseColumn}]))",
            DataValueType.DateTime => $"IIF(IsNull([{columnMapping.DatabaseColumn}]), Null, CDATE([{columnMapping.DatabaseColumn}]))",
            _ => throw new ArgumentOutOfRangeException(nameof(columnMapping), columnMapping.DataType, null)
        };
    }
}