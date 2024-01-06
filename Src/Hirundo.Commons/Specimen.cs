namespace Hirundo.Commons;

/// <summary>
///     Klasa reprezentująca zaobserwowane zwierzę oznaczone obrączką z unikalnym numerem. Osobnik charakteryzuje się
///     zestawem cech, które są zapisane w bazie danych. Osobnik może być zaobserwowany wiele razy w różnych miejscach i
///     czasach.
/// </summary>
public class Specimen
{
    /// <summary>
    ///     Unikalny identyfikator osobnika, np. numer obrączki.
    /// </summary>
    public string Identifier { get; set; } = null!;

    /// <summary>
    ///     Lista obserwacji osobnika.
    /// </summary>
    public List<Observation> Observations { get; set; } = [];
}