namespace Hirundo.Databases;

/// <summary>
///     Parametry bazy danych Access.
/// </summary>
public class AccessDatabaseParameters
{
    /// <summary>
    ///     Nazwa pliku bazy danych. Obsługuje pliki .mdb.
    /// </summary>
    public string FilePath { get; set; } = null!;

    /// <summary>
    ///     Nazwa tabeli w bazie danych. Należy podawać prostą formę, bez nawiasów kwadratowych.
    /// </summary>
    public string TableName { get; set; } = null!;

    /// <summary>
    ///     Lista kolumn z danymi.
    /// </summary>
    public IList<DatabaseColumn> ValuesColumns { get; set; } = new List<DatabaseColumn>();
}