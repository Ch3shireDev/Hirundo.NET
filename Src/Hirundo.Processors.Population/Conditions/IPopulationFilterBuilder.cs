using Hirundo.Commons;

namespace Hirundo.Processors.Population.Conditions;

public interface IPopulationFilterBuilder
{
    public IPopulationFilter GetPopulationFilter(Specimen returningSpecimen);
}