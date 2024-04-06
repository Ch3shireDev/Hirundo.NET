using Hirundo.Commons.Models;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population;

/// <summary>
///     Prosta implementacja procesora populacji.
/// </summary>
public class PopulationProcessor(IPopulationConditionBuilder conditionBuilder, CancellationToken? cancellationToken) : IPopulationProcessor
{
    public IEnumerable<Specimen> GetPopulation(Specimen returningSpecimen, IEnumerable<Specimen> totalSpecimens)
    {
        cancellationToken?.ThrowIfCancellationRequested();
        var @internal = conditionBuilder.GetPopulationCondition(returningSpecimen);
        return totalSpecimens.Where(@internal.IsAccepted).ToList();
    }
}