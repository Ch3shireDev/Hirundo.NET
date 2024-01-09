using Hirundo.Commons;

namespace Hirundo.Processors.Population.Conditions;

public sealed class CompositePopulationFilterBuilder(IEnumerable<IPopulationFilterBuilder> builders) : IPopulationFilterBuilder
{
    public IPopulationFilter GetPopulationFilter(Specimen returningSpecimen)
    {
        var filters = builders.Select(builder => builder.GetPopulationFilter(returningSpecimen)).ToArray();

        return new CompositePopulationFilter(filters);
    }

    private sealed class CompositePopulationFilter(IEnumerable<IPopulationFilter> filters) : IPopulationFilter
    {
        public bool IsAccepted(Specimen specimen)
        {
            return filters.All(filter => filter.IsAccepted(specimen));
        }
    }
}