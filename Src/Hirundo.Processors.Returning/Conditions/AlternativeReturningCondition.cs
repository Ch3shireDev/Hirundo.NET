using Hirundo.Commons;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription("Alternative", true)]
public class AlternativeReturningCondition : IReturningSpecimenCondition
{
    public IList<IReturningSpecimenCondition> Conditions { get; } = new List<IReturningSpecimenCondition>();

    public AlternativeReturningCondition(params IReturningSpecimenCondition[] conditions)
    {
        Conditions = conditions;
    }

    public AlternativeReturningCondition(IList<IReturningSpecimenCondition> conditions)
    {
        Conditions = conditions;
    }

    public AlternativeReturningCondition() { }

    public bool IsReturning(Specimen specimen)
    {
        return Conditions.Any(c => c.IsReturning(specimen));
    }
}
