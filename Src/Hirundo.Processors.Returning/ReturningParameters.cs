using Hirundo.Commons.Helpers;
using Hirundo.Processors.Returning.Conditions;
using System.Text;

namespace Hirundo.Processors.Returning;

public class ReturningParameters : ISelfExplainer
{
    public ReturningParameters()
    {
    }

    public ReturningParameters(IList<IReturningSpecimenCondition> conditions)
    {
        Conditions = conditions;
    }

    public IList<IReturningSpecimenCondition> Conditions { get; init; } = [];

    public string Explain()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Konfiguracja powracających osobników:");
        sb.AppendLine($"Określanie powracających osobników jest przeprowadzane na podstawie {Conditions.Count} warunków.");
        sb.AppendLine("Warunki:");

        foreach (var condition in Conditions)
        {
            sb.AppendLine(ExplainerHelpers.Explain(condition));
        }

        sb.AppendLine();
        return sb.ToString();
    }
}