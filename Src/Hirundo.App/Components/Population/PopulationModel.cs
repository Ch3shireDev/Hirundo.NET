using Hirundo.Processors.Population;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.App.Components.Population;

public class PopulationModel
{
    public PopulationProcessorParameters ConfigPopulation { get; set; } = null!;
    public IList<IPopulationFilterBuilder> Conditions => ConfigPopulation.Conditions;
}