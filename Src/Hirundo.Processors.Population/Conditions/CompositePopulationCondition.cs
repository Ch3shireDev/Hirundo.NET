using Hirundo.Commons.Models;

namespace Hirundo.Processors.Population.Conditions;

public sealed class CompositePopulationCondition(IEnumerable<IPopulationCondition> builders) : IPopulationCondition
{
    public IPopulationConditionClosure GetPopulationConditionClosure(Specimen returningSpecimen)
    {
        var filters = builders.Select(builder => builder.GetPopulationConditionClosure(returningSpecimen)).ToArray();

        return new CompositePopulationConditionClosure(filters);
    }

    private sealed class CompositePopulationConditionClosure(IEnumerable<IPopulationConditionClosure> filters) : IPopulationConditionClosure
    {
        public bool IsAccepted(Specimen specimen)
        {
            return filters.All(filter => filter.IsAccepted(specimen));
        }
    }
}