namespace Hirundo.Commons.Models;

/// <summary>
///     Klasa reprezentująca zaobserwowane zwierzę oznaczone obrączką z unikalnym numerem. Osobnik charakteryzuje się
///     zestawem cech, które są zapisane w bazie danych. Osobnik może być zaobserwowany wiele razy w różnych miejscach i
///     czasach.
/// </summary>
public class Specimen(object identifier, IList<Observation> observations)
{
    /// <summary>
    ///     Unikalny identyfikator osobnika, np. numer obrączki.
    /// </summary>
    public object Identifier { get; set; } = identifier;

    /// <summary>
    ///     Lista obserwacji osobnika.
    /// </summary>
    public IList<Observation> Observations { get; } = observations;

    public string[] GetHeaders()
    {
        return Observations.FirstOrDefault()?.GetHeaders() ?? [];
    }

    public object?[] GetValues()
    {
        return Observations.FirstOrDefault()?.GetValues() ?? [];
    }

    public object? GetValue(string header)
    {
        return Observations.FirstOrDefault()?.GetValue(header);
    }
}