namespace Hirundo.Filters.Specimens;

/// <summary>
///     Budowniczy filtrów powracających osobników.
/// </summary>
public class ReturningSpecimenFiltersBuilder
{
    private readonly List<IReturningSpecimenFilter> _conditions = [];

    /// <summary>
    ///     Buduje filtr powracających osobników.
    /// </summary>
    /// <returns></returns>
    public IReturningSpecimenFilter Build()
    {
        return new CompositeReturningSpecimenFilter([.._conditions]);
    }

    public ReturningSpecimenFiltersBuilder WithConditions(IEnumerable<IReturningSpecimenFilter> returningSpecimensConditions)
    {
        _conditions.AddRange(returningSpecimensConditions);
        return this;
    }
}