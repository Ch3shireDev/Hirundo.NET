using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population;

public class PopulationParameters
{
    public IList<IPopulationConditionBuilder> Conditions { get; init; } = [];
}