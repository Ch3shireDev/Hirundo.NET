using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning;

public interface IReturningSpecimenConditionsBuilder
{
    /// <summary>
    ///     Buduje filtr powracających osobników.
    /// </summary>
    /// <returns></returns>
    IReturningSpecimenCondition Build();

    IReturningSpecimenConditionsBuilder WithReturningSpecimensConditions(IEnumerable<IReturningSpecimenCondition> returningSpecimensConditions);
}