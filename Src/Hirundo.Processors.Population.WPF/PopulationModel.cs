using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF;

public class PopulationModel
{
    public PopulationProcessorParameters ConfigPopulation { get; set; } = new();
    public IList<IPopulationFilterBuilder> Conditions => ConfigPopulation.Conditions;
}