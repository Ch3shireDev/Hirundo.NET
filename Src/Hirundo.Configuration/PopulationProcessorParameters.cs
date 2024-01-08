using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Configuration;

public class PopulationProcessorParameters
{
    public IPopulationFilterBuilder[] Conditions { get; set; } = [];
}