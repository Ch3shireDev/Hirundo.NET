using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population;

public class PopulationProcessorParameters
{
    public IList<IPopulationFilterBuilder> Conditions { get; init; } = [];
}