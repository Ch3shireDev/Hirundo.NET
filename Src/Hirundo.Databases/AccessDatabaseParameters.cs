using Hirundo.Commons;
using Hirundo.Commons.Models;
using Hirundo.Databases.Conditions;

namespace Hirundo.Databases;

/// <summary>
///     Parametry bazy danych Access.
/// </summary>
[TypeDescription("Access", "Źródło danych Access", "Źródło danych z pliku Access.")]
public class AccessDatabaseParameters : IDatabaseParameters, IFileSource
{
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
}