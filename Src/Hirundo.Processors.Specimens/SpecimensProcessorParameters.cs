namespace Hirundo.Processors.Specimens;

/// <summary>
///     Parametry procesora osobników, zawierajace m.in. nazwę kolumny danych z identyfikatorem osobnika, np. numerem
///     obrączki.
/// </summary>
public class SpecimensProcessorParameters
{
    /// <summary>
    ///     Jednoznaczny identyfikator osobnika, np. numer obrączki.
    /// </summary>
    public string SpecimenIdentifier { get; set; } = null!;

    /// <summary>
    ///     Czy należy uwzględniać przypadki, gdy wartość identyfikatora osobnika jest pusta? Domyślnie <c>false</c>.
    /// </summary>
    public bool IncludeEmptyValues { get; set; }
}