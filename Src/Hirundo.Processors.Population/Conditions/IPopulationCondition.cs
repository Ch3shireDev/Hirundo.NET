using Hirundo.Commons.Models;

namespace Hirundo.Processors.Population.Conditions;

/// <summary>
///     Warunki populacji — na podstawie parametrów wejściowych podejmowana jest decyzja, czy obserwowany dany
///     osobnik należy do populacji.
/// </summary>
public interface IPopulationCondition
{
    /// <summary>
    ///     Generuje mechanizm sprawdzający, czy dany osobnik należy do populacji.
    /// </summary>
    /// <param name="returningSpecimen">Powracający osobnik, dla którego będziemy znajdywać osobniki należące do populacji.</param>
    /// <returns></returns>
    public IPopulationConditionClosure GetPopulationConditionClosure(Specimen returningSpecimen);

}
public interface IPopulationConditionClosure
{
    /// <summary>
    ///     Informacja, czy dany osobnik należy do populacji.
    /// </summary>
    /// <param name="specimen">Osobnik.</param>
    /// <returns>Informacja, czy osobnik należy do populacji.</returns>
    bool IsAccepted(Specimen specimen);
}