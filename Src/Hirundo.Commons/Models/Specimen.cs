namespace Hirundo.Commons.Models;

/// <summary>
///     Klasa reprezentująca zaobserwowane zwierzę oznaczone obrączką z unikalnym numerem. Osobnik charakteryzuje się
///     zestawem cech, które są zapisane w bazie danych. Osobnik może być zaobserwowany wiele razy w różnych miejscach i
///     czasach.
/// </summary>
public class Specimen(string ring, IList<Observation> observations)
{
    /// <summary>
    ///     Unikalny identyfikator osobnika, np. numer obrączki.
    /// </summary>
    public string Ring { get; set; } = ring;

    /// <summary>
    ///     Lista obserwacji osobnika.
    /// </summary>
    public IList<Observation> Observations { get; } = [.. observations.OrderBy(o => o.Date)];

    public string[] GetHeaders()
    {
        return Observations.FirstOrDefault()?.Headers.ToArray() ?? [];
    }

    public object?[] GetValues()
    {
        return Observations.FirstOrDefault()?.Values.ToArray() ?? [];
    }

    public object? GetValue(string header)
    {
        return Observations.FirstOrDefault()?.GetValue(header);
    }
}