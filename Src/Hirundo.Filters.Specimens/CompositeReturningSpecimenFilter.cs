using Hirundo.Commons;

namespace Hirundo.Filters.Specimens;

/// <summary>
///     Implementacja filtra zwracającego wszystkie okazy.
/// </summary>
public class CompositeReturningSpecimenFilter(params IReturningSpecimenFilter[] conditions) : IReturningSpecimenFilter
{
    public bool IsReturning(Specimen specimen)
    {
        return conditions.All(condition => condition.IsReturning(specimen));
    }
}