using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning;

/// <summary>
///     Budowniczy filtrów powracających osobników.
/// </summary>
public class ReturningSpecimenConditionsBuilder
{
    private readonly List<IReturningSpecimenCondition> _conditions = [];

    /// <summary>
    ///     Buduje filtr powracających osobników.
    /// </summary>
    /// <returns></returns>
    public IReturningSpecimenCondition Build()
    {
        return new CompositeReturningSpecimenCondition([.. _conditions]);
    }

    public ReturningSpecimenConditionsBuilder WithReturningSpecimensConditions(IEnumerable<IReturningSpecimenCondition> returningSpecimensConditions)
    {
        _conditions.AddRange(returningSpecimensConditions);
        return this;
    }
}