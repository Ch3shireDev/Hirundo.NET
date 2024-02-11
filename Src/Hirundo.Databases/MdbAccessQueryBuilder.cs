using System.Globalization;
using System.Text;
using Hirundo.Databases.Conditions;

namespace Hirundo.Databases;

/// <summary>
///     Budowniczy zapytań dla bazy danych Access 2003 (pliki .mdb). Zapytanie jest tworzone na podstawie nazwy tabeli oraz
///     <see cref="ColumnMapping" />.
/// </summary>
public class MdbAccessQueryBuilder(string rowSeparator = " ")
{
    private readonly List<ColumnMapping> _columns = [];
    private readonly List<DatabaseCondition> _conditions = [];
    private string? _tableName;

    public string RowSeparator { get; } = rowSeparator;

    /// <summary>
    ///     Ustalana jest nazwa tabeli, do której odwołuje się zapytanie SELECT.
    ///     Nazwa tabeli nie powinna zawierać nawiasów kwadratowych.
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public MdbAccessQueryBuilder WithTable(string tableName)
    {
        _tableName = tableName;
        return this;
    }

    /// <summary>
    ///     Dodawana jest lista kolumn (bądź wyrażeń) do zapytania SELECT. Zależnie od podanego typu danych,
    ///     wartości są konwertowane na poziomie zapytania SQL.
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    public MdbAccessQueryBuilder WithColumns(IEnumerable<ColumnMapping> columns)
    {
        _columns.AddRange(columns);
        return this;
    }

    /// <summary>
    ///     Dodawana jest pojedyncza kolumna (bądź wyrażenie) do zapytania SELECT. Zależnie od podanego typu danych,
    ///     wartość jest konwertowana na poziomie zapytania SQL.
    /// </summary>
    /// <param name="columnMapping"></param>
    /// <returns></returns>
    public MdbAccessQueryBuilder WithColumn(ColumnMapping columnMapping)
    {
        _columns.Add(columnMapping);
        return this;
    }

    /// <summary>
    ///     Dodawany jest warunek do zapytania SELECT (klauzula WHERE).
    /// </summary>
    /// <param name="databaseCondition"></param>
    /// <returns></returns>
    public MdbAccessQueryBuilder WithCondition(DatabaseCondition databaseCondition)
    {
        _conditions.Add(databaseCondition);
        return this;
    }

    /// <summary>
    ///     Dodawane są warunki do zapytania SELECT (klauzula WHERE).
    /// </summary>
    /// <param name="parametersConditions"></param>
    /// <returns></returns>
    public MdbAccessQueryBuilder WithConditions(IEnumerable<DatabaseCondition> parametersConditions)
    {
        _conditions.AddRange(parametersConditions);
        return this;
    }

    /// <summary>
    ///     Przygotowywana jest kwerenda SQL na podstawie parametrów wejściowych.
    /// </summary>
    /// <returns></returns>
    public string Build()
    {
        ArgumentNullException.ThrowIfNull(_tableName);

        var stringBuilder = new StringBuilder();
        stringBuilder.Append("SELECT");
        stringBuilder.Append(RowSeparator);
        stringBuilder.Append(string.Join($",{RowSeparator}", _columns.Select(GetSqlColumnExpression)));
        stringBuilder.Append(RowSeparator);
        stringBuilder.Append(CultureInfo.InvariantCulture, $"FROM [{_tableName ?? ""}]");
        stringBuilder.Append(GetWhereClause());
        return stringBuilder.ToString();
    }

    private string GetWhereClause()
    {
        if (_conditions.Count == 0) return string.Empty;

        var stringBuilder2 = new StringBuilder();
        stringBuilder2.Append(RowSeparator);
        stringBuilder2.Append("WHERE");
        stringBuilder2.Append(RowSeparator);

        foreach (var condition in _conditions)
        {
            var isFirst = condition == _conditions.First();

            if (!isFirst)
            {
                var logicOperator = GetLogicOperator(condition.ConditionOperator);
                stringBuilder2.Append(RowSeparator);
                stringBuilder2.Append(CultureInfo.InvariantCulture, $"{logicOperator}");
                stringBuilder2.Append(RowSeparator);
            }

            stringBuilder2.Append(CultureInfo.InvariantCulture, $"{GetColumnForWhereQuery(condition)} {TwoArgumentOperator(condition.Type)} {GetValueForSqlWhereStatement(condition.Value)}");
        }

        var result = stringBuilder2.ToString();
        return result;
    }

    private static string GetColumnForWhereQuery(DatabaseCondition condition)
    {
        // TODO: Refactor date conversion
        if (condition.DatabaseColumn == "DATE2") return $"CDATE([{condition.DatabaseColumn}])";
        return $"[{condition.DatabaseColumn}]";
    }

    private static string TwoArgumentOperator(DatabaseConditionType conditionType)
    {
        return conditionType switch
        {
            DatabaseConditionType.IsEqual => "=",
            DatabaseConditionType.IsGreaterThan => ">",
            DatabaseConditionType.IsGreaterOrEqual => ">=",
            DatabaseConditionType.IsLowerThan => "<",
            DatabaseConditionType.IsLowerOrEqual => "<=",
            _ => throw new ArgumentOutOfRangeException(nameof(conditionType), conditionType, null)
        };
    }

    private static string GetLogicOperator(DatabaseConditionOperator conditionOperator)
    {
        return conditionOperator switch
        {
            DatabaseConditionOperator.And => "AND",
            DatabaseConditionOperator.Or => "OR",
            _ => throw new ArgumentOutOfRangeException(nameof(conditionOperator), conditionOperator, null)
        };
    }

    private static string GetValueForSqlWhereStatement(object value)
    {
        return value switch
        {
            string stringValue => DateTime.TryParse(stringValue, out var date) ? GetDateQuery(date) : $"'{stringValue}'",
            int intValue => intValue.ToString(CultureInfo.InvariantCulture),
            long longValue => longValue.ToString(CultureInfo.InvariantCulture),
            double doubleValue => doubleValue.ToString(CultureInfo.InvariantCulture),
            DateTime dateTimeValue => GetDateQuery(dateTimeValue),
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    private static string GetDateQuery(DateTime date)
    {
        return $"DateSerial({date.Year}, {date.Month}, {date.Day})";
    }

    /// <summary>
    ///     Metoda odpowiedzialna za konwersję danych na poziomie zapytania SQL. Używane funkcje sprawiają, że
    ///     dane są przetwarzane około 3x dłużej (z 40s to 100s).
    /// </summary>
    /// <param name="columnMapping"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static string GetSqlColumnExpression(ColumnMapping columnMapping)
    {
        return columnMapping.DataType switch
        {
            DataValueType.Undefined => $"[{columnMapping.DatabaseColumn}]",
            DataValueType.Text => $"IIF(IsNull([{columnMapping.DatabaseColumn}]), Null, CSTR([{columnMapping.DatabaseColumn}]))",
            DataValueType.ShortInt => $"IIF(IsNull([{columnMapping.DatabaseColumn}]), Null, CINT([{columnMapping.DatabaseColumn}]))",
            DataValueType.LongInt => $"IIF(IsNull([{columnMapping.DatabaseColumn}]), Null, CLNG([{columnMapping.DatabaseColumn}]))",
            DataValueType.Numeric => $"IIF(IsNull([{columnMapping.DatabaseColumn}]), Null, CDBL([{columnMapping.DatabaseColumn}]))",
            DataValueType.DateTime => $"IIF(IsNull([{columnMapping.DatabaseColumn}]), Null, CDATE([{columnMapping.DatabaseColumn}]))",
            _ => throw new ArgumentOutOfRangeException(nameof(columnMapping), columnMapping.DataType, null)
        };
    }
}