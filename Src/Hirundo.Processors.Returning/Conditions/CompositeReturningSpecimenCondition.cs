using Hirundo.Commons;

namespace Hirundo.Processors.Returning.Conditions;

/// <summary>
///     Implementacja filtra zwracającego wszystkie okazy.
/// </summary>
public class CompositeReturningSpecimenCondition : IReturningSpecimenCondition
{
    public CompositeReturningSpecimenCondition(params IReturningSpecimenCondition[] conditions) : this(conditions, null) { }

    public CompositeReturningSpecimenCondition(IList<IReturningSpecimenCondition> conditions, CancellationToken? cancellationToken = null)
    {
        ArgumentNullException.ThrowIfNull(conditions);
        Conditions = conditions;
        CancellationToken = cancellationToken;
    }

    public IList<IReturningSpecimenCondition> Conditions { get; }
    public CancellationToken? CancellationToken { get; }

    public bool IsReturning(Specimen specimen)
    {
        ArgumentNullException.ThrowIfNull(specimen);
        CancellationToken?.ThrowIfCancellationRequested();
        return Conditions.All(condition => condition.IsReturning(specimen));
    }
}