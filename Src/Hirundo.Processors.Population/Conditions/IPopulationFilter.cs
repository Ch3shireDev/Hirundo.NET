using Hirundo.Commons;

namespace Hirundo.Processors.Population.Conditions;

/// <summary>
///     Filtr populacji - na podstawie parametrów wejściowych podejmowana jest decyzja, czy obserwowany dany
///     osobnik należy do populacji.
/// </summary>
public interface IPopulationFilter
{
    /// <summary>
    ///     Informacja, czy dany osobnik należy do populacji.
    /// </summary>
    /// <param name="specimen">Osobnik.</param>
    /// <returns>Informacja, czy osobnik należy do populacji.</returns>
    bool IsAccepted(Specimen specimen);
}