namespace Hirundo.Processors.Returning.Conditions;

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

    public ReturningSpecimenFiltersBuilder WithReturningSpecimensConditions(IEnumerable<IReturningSpecimenFilter> returningSpecimensConditions)
    {
        _conditions.AddRange(returningSpecimensConditions);
        return this;
    }
}