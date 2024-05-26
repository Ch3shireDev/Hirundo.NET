using Hirundo.Commons.Models;

namespace Hirundo.Processors.Population.Conditions;

internal sealed class CustomClosure(Func<Specimen, bool> closure) : IPopulationConditionClosure
{
    public bool IsAccepted(Specimen specimen)
    {
        return closure(specimen);
    }
}