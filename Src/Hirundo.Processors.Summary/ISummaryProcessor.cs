using Hirundo.Commons;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Summary;

/// <summary>
///     Przetarza dane o powracającym osobniku i tworzy na ich podstawie podsumowanie.
/// </summary>
public interface ISummaryProcessor
{
    /// <summary>
    ///     Zwraca podsumowanie dla danego powracającego osobnika.
    /// </summary>
    /// <param name="returningSpecimen">Osobnik powracający.</param>
    /// <returns>Podsumowanie.</returns>
    ReturningSpecimenSummary GetSummary(Specimen returningSpecimen);
}