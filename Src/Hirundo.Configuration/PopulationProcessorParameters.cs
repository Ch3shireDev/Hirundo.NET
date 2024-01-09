using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Configuration;

public class PopulationProcessorParameters
{
    public IList<IPopulationFilterBuilder> Conditions { get; init; } = [];
}