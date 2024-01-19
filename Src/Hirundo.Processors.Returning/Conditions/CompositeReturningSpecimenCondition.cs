using Hirundo.Commons;

namespace Hirundo.Processors.Returning.Conditions;

/// <summary>
///     Implementacja filtra zwracającego wszystkie okazy.
/// </summary>
public class CompositeReturningSpecimenCondition(params IReturningSpecimenCondition[] conditions) : IReturningSpecimenCondition
{
    public bool IsReturning(Specimen specimen)
    {
        return conditions.All(condition => condition.IsReturning(specimen));
    }
}