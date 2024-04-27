using Hirundo.Commons;
using Hirundo.Commons.Models;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription(
    "Alternative",
    "Czy przynajmniej jeden z warunków został spełniony?",
    "Aby osobnik był uznany za powracający, musi być spełniony co najmniej jeden z warunków.",
    true)]
public class AlternativeReturningCondition : IReturningSpecimenCondition
{
    public IList<IReturningSpecimenCondition> Conditions { get; } = [];

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
