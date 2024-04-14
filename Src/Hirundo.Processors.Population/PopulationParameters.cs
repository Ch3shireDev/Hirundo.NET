using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population;

public class PopulationParameters
{
    public IList<IPopulationCondition> Conditions { get; init; } = [];
}