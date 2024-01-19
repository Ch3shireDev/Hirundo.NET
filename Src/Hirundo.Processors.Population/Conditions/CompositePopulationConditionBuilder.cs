using Hirundo.Commons;

namespace Hirundo.Processors.Population.Conditions;

public sealed class CompositePopulationConditionBuilder(IEnumerable<IPopulationConditionBuilder> builders) : IPopulationConditionBuilder
{
    public IPopulationCondition GetPopulationCondition(Specimen returningSpecimen)
    {
        var filters = builders.Select(builder => builder.GetPopulationCondition(returningSpecimen)).ToArray();

        return new CompositePopulationCondition(filters);
    }

    private sealed class CompositePopulationCondition(IEnumerable<IPopulationCondition> filters) : IPopulationCondition
    {
        public bool IsAccepted(Specimen specimen)
        {
            return filters.All(filter => filter.IsAccepted(specimen));
        }
    }
}