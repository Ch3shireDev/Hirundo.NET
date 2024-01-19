using Hirundo.Commons;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population;

/// <summary>
///     Prosta implementacja procesora populacji.
/// </summary>
public class PopulationProcessor(IPopulationConditionBuilder conditionBuilder) : IPopulationProcessor
{
    public IEnumerable<Specimen> GetPopulation(Specimen returningSpecimen, IEnumerable<Specimen> totalSpecimens)
    {
        var @internal = conditionBuilder.GetPopulationCondition(returningSpecimen);
        return totalSpecimens.Where(@internal.IsAccepted).ToList();
    }
}