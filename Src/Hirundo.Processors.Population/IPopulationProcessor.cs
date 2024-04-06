using Hirundo.Commons.Models;

namespace Hirundo.Processors.Population;

/// <summary>
///     Interfejs procesora grupującego populację dla danego osobnika powracającego.
/// </summary>
public interface IPopulationProcessor
{
    /// <summary>
    ///     Zwraca populację dla danego osobnika powracającego.
    /// </summary>
    /// <param name="returningSpecimen">Osobnik powracający.</param>
    /// <param name="totalSpecimens">Lista wszystkich osobników.</param>
    /// <returns></returns>
    IEnumerable<Specimen> GetPopulation(Specimen returningSpecimen, IEnumerable<Specimen> totalSpecimens);
}