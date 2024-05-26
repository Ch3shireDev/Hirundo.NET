using Hirundo.Processors.Returning.Conditions;
using Serilog;

namespace Hirundo.Processors.Returning;

/// <summary>
///     Budowniczy filtrów powracających osobników.
/// </summary>
public class ReturningSpecimenConditionsBuilder : IReturningSpecimenConditionsBuilder
{
    private readonly List<IReturningSpecimenCondition> _conditions = [];
    private CancellationToken? _cancellationToken;

    /// <summary>
    ///     Buduje filtr powracających osobników.
    /// </summary>
    /// <returns></returns>
    public IReturningSpecimenCondition Build()
    {
        Log.Information("Budowanie warunków powracających osobników. Liczba warunków: {_conditionsCount}.", _conditions.Count);
        return new CompositeReturningSpecimenCondition([.. _conditions], _cancellationToken);
    }

    public IReturningSpecimenConditionsBuilder NewBuilder()
    {
        return new ReturningSpecimenConditionsBuilder();
    }

    public IReturningSpecimenConditionsBuilder WithCancellationToken(CancellationToken? cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return this;
    }

    public IReturningSpecimenConditionsBuilder WithReturningSpecimensConditions(IEnumerable<IReturningSpecimenCondition> returningSpecimensConditions)
    {
        _conditions.AddRange(returningSpecimensConditions);
        return this;
    }
}