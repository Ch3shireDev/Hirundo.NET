namespace Hirundo.Processors.Returning.Conditions;

/// <summary>
///     Budowniczy filtrów powracających osobników.
/// </summary>
public class ReturningSpecimenFiltersBuilder
{
    private readonly List<IReturningSpecimenCondition> _conditions = [];

    /// <summary>
    ///     Buduje filtr powracających osobników.
    /// </summary>
    /// <returns></returns>
    public IReturningSpecimenCondition Build()
    {
        return new CompositeReturningSpecimenCondition([.._conditions]);
    }

    public ReturningSpecimenFiltersBuilder WithReturningSpecimensConditions(IEnumerable<IReturningSpecimenCondition> returningSpecimensConditions)
    {
        _conditions.AddRange(returningSpecimensConditions);
        return this;
    }
}