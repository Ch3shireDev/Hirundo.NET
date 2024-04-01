namespace Hirundo.Processors.Specimens;

/// <summary>
///     Parametry procesora osobników, zawierajace m.in. nazwę kolumny danych z identyfikatorem osobnika, np. numerem
///     obrączki.
/// </summary>
public class SpecimensParameters
{
    /// <summary>
    ///     Jednoznaczny identyfikator osobnika, np. numer obrączki.
    /// </summary>
    public string SpecimenIdentifier { get; set; } = null!;
}