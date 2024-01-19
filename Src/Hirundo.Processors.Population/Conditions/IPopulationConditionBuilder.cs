using Hirundo.Commons;

namespace Hirundo.Processors.Population.Conditions;

public interface IPopulationConditionBuilder
{
    public IPopulationCondition GetPopulationCondition(Specimen returningSpecimen);
}