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
        return new CompositeReturningSpecimenFilter([.._conditions]);
    }

    public ReturningSpecimenFiltersBuilder WithConditions(List<IReturningSpecimenFilter> returningSpecimensConditions)
    {
        _conditions = returningSpecimensConditions;
        return this;
    }

    private List<IReturningSpecimenFilter> _conditions = [];
}