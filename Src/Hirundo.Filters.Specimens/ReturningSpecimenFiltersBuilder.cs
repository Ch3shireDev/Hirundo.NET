namespace Hirundo.Filters.Specimens;

/// <summary>
///     Budowniczy filtrów powracających osobników.
/// </summary>
public class ReturningSpecimenFiltersBuilder
{
    /// <summary>
    ///     Buduje filtr powracających osobników.
    /// </summary>
    /// <returns></returns>
    public IReturningSpecimenFilter Build()
    {
        return new CompositeReturningSpecimenFilter();
    }
}