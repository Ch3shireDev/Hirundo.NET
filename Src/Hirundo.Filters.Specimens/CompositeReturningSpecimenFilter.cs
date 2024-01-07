using Hirundo.Commons;

namespace Hirundo.Filters.Specimens;

/// <summary>
///     Implementacja filtra zwracającego wszystkie okazy.
/// </summary>
public class CompositeReturningSpecimenFilter : IReturningSpecimenFilter
{
    public bool IsReturning(Specimen specimen)
    {
        return true;
    }
}