using Hirundo.Commons.Models;

namespace Hirundo.Processors.Population.Conditions;

public interface IPopulationConditionBuilder
{
    public IPopulationCondition GetPopulationCondition(Specimen returningSpecimen);
}