using Hirundo.Commons;

namespace Hirundo.Filters.Specimens;

/// <summary>
///     Implementacja filtra zwracającego wszystkie okazy.
/// </summary>
public class ReturningSpecimenFilters : IReturningSpecimenFilter
{
    public bool IsReturningSpecimen(Specimen specimen)
    {
        return true;
    }
}