using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;
using System.Text;

namespace Hirundo.Processors.Returning.Conditions;

[TypeDescription(
    "Alternative",
    "Czy przynajmniej jeden z warunków został spełniony?",
    "Aby osobnik był uznany za powracający, musi być spełniony co najmniej jeden z warunków.",
    true)]
public class AlternativeReturningCondition : IReturningSpecimenCondition, ISelfExplainer
{
    public AlternativeReturningCondition(params IReturningSpecimenCondition[] conditions)
    {
        Conditions = conditions;
    }

    public AlternativeReturningCondition(IList<IReturningSpecimenCondition> conditions)
    {
        Conditions = conditions;
    }

    public AlternativeReturningCondition()
    {
    }

    public IList<IReturningSpecimenCondition> Conditions { get; } = [];

    public bool IsReturning(Specimen specimen)
    {
        return Conditions.Any(c => c.IsReturning(specimen));
    }

    public string Explain()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Warunki alternatywne (jeden lub drugi lub obydwa):");

        foreach (var condition in Conditions)
        {
            var results = ExplainerHelpers.Explain(condition).Split('\n');

            foreach (var result in results)
            {
                sb.AppendLine("    " + result + "\n");
            }
        }

        return sb.ToString();
    }
}