using Hirundo.Commons;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population;

/// <summary>
///     Prosta implementacja procesora populacji.
/// </summary>
public class PopulationProcessor(IPopulationFilterBuilder conditionBuilder) : IPopulationProcessor
{
    public IEnumerable<Specimen> GetPopulation(Specimen returningSpecimen, IEnumerable<Specimen> totalSpecimens)
    {
        var condition = conditionBuilder.GetPopulationFilter(returningSpecimen);
        return totalSpecimens.Where(condition.IsAccepted).ToList();
    }
}