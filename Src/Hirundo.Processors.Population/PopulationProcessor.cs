using Hirundo.Commons;

namespace Hirundo.Processors.Population;

/// <summary>
///     Prosta implementacja procesora populacji.
/// </summary>
public class PopulationProcessor : IPopulationProcessor
{
    public PopulationData GetPopulation(Specimen returningSpecimen, IEnumerable<Specimen> totalSpecimens)
    {
        var specimensList = totalSpecimens.ToList();

        return new PopulationData
        {
            Specimens = specimensList
        };
    }
}