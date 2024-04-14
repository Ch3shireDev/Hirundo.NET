using Hirundo.Commons.Models;

namespace Hirundo.Processors.Population.Conditions;

internal class CustomClosure(Func<Specimen, bool> closure) : IPopulationConditionClosure
{
    public bool IsAccepted(Specimen specimen)
    {
        return closure(specimen);
    }
}