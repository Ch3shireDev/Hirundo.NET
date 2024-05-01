using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;
using Hirundo.Databases.Conditions;
using System.Globalization;
using System.Text;

namespace Hirundo.Databases;

/// <summary>
///     Parametry bazy danych Access.
/// </summary>
[TypeDescription("Access", "Źródło danych Access", "Źródło danych z pliku Access.")]
public class AccessDatabaseParameters : IDatabaseParameters, IFileSource, ISelfExplainer
{
    private readonly string ColumnInfo = "Zapisuje kolumnę {0} jako {1}, typu {2}.";

    private readonly string DataSourceInfo = "Pobiera dane z pliku {0}, z tabeli {1}.";

    /// <summary>
    ///     Nazwa tabeli w bazie danych. Należy podawać prostą formę, bez nawiasów kwadratowych.
    /// </summary>
    public string Table { get; set; } = null!;

    /// <summary>
    ///     Nazwa kolumny identyfikującej gatunek.
    /// </summary>
    public string SpeciesIdentifier { get; set; } = null!;

    /// <summary>
    ///     Lista warunków do spełnienia przez kolumny danych (klauzula WHERE).
    /// </summary>
    public IList<DatabaseCondition> Conditions { get; init; } = [];

    /// <summary>
    ///     Nazwa kolumny identyfikującej osobnika.
    /// </summary>
    public string RingIdentifier { get; set; } = null!;

    /// <summary>
    ///     Nazwa kolumny identyfikującej datę.
    /// </summary>
    public string DateIdentifier { get; set; } = null!;

    /// <summary>
    ///     Lista kolumn z danymi.
    /// </summary>
    public IList<ColumnParameters> Columns { get; init; } = [];

    /// <summary>
    ///     Nazwa pliku bazy danych. Obsługuje pliki .mdb.
    /// </summary>
    public string Path { get; set; } = null!;

    public string Explain()
    {
        var sb = new StringBuilder();

        sb.AppendLine("Konfiguracja bazy danych Access:");
        sb.AppendLine(string.Format(CultureInfo.InvariantCulture, DataSourceInfo, Path, Table));
        sb.AppendLine();
        sb.AppendLine("Kolumny:");

        foreach (var column in Columns)
        {
            sb.AppendLine(string.Format(CultureInfo.InvariantCulture, ColumnInfo, column.DatabaseColumn, column.ValueName,
                ExplainDataType(column.DataType
                )
            ));
        }

        sb.AppendLine();
        sb.AppendLine("Warunki:");

        foreach (var condition in Conditions)
        {
            sb.AppendLine(CultureInfo.InvariantCulture, $"Kolumna {condition.DatabaseColumn} {GetOperationName(condition.Type)} {condition.Value}");
        }

        return sb.ToString();
    }

    private static string GetOperationName(DatabaseConditionType condition)
    {
        return condition switch
        {
            DatabaseConditionType.IsEqual => "równa się",
            DatabaseConditionType.IsNotEqual => "nie równa się",
            DatabaseConditionType.IsGreaterThan => "jest większa niż",
            DatabaseConditionType.IsGreaterOrEqual => "jest większa lub równa",
            DatabaseConditionType.IsLowerThan => "jest mniejsza niż",
            DatabaseConditionType.IsLowerOrEqual => "jest mniejsza lub równa",
            _ => throw new NotImplementedException()
        };
    }

    private static string ExplainDataType(DataType dataType)
    {
        return dataType switch
        {
            DataType.Number => "liczba całkowita",
            DataType.Text => "tekst",
            DataType.Undefined => "nieokreślony",
            DataType.Date => "data",
            DataType.Numeric => "liczba wymierna",
            _ => throw new NotImplementedException()
        };
    }
}