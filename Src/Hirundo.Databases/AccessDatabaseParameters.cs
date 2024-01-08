namespace Hirundo.Databases;

/// <summary>
///     Parametry bazy danych Access.
/// </summary>
public class AccessDatabaseParameters : DatabaseParameters
{
    /// <summary>
    ///     Nazwa pliku bazy danych. Obsługuje pliki .mdb.
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    ///     Nazwa tabeli w bazie danych. Należy podawać prostą formę, bez nawiasów kwadratowych.
    /// </summary>
    public string Table { get; set; } = null!;

    /// <summary>
    ///     Lista kolumn z danymi.
    /// </summary>
    public IList<ColumnMapping> Columns { get; set; } = new List<ColumnMapping>();
}