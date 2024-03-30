using Hirundo.Commons.Models;

namespace Hirundo.Processors.Observations;

/// <summary>
///     Warunek obserwacji. Podczas przetwarzania danych z bazy danych, obserwacje są filtrowane warunkami zadanymi przez
///     użytkownika. Te warunki są reprezentowane przez obiekty implementujące ten interfejs.
/// </summary>
public interface IObservationCondition
{
    /// <summary>
    ///     Sprawdza, czy obserwacja spełnia warunki filtru.
    /// </summary>
    /// <param name="observation">Obserwacja.</param>
    /// <returns>Odpowiedź, czy obserwacja spełnia zadany warunek.</returns>
    public bool IsAccepted(Observation observation);
}