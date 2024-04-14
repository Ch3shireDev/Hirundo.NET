using Hirundo.Commons.Helpers;
using Hirundo.Processors.Population;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.App.Explainers.Population;
public class PopulationExplainer : ParametersExplainer<PopulationParameters>
{
    public override string Explain(PopulationParameters parameters)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Konfiguracja populacji:");
        sb.AppendLine($"Populacja jest dobierana do powracającego osobnika na podstawie {parameters.Conditions.Count} warunków.");

        foreach (var condition in parameters.Conditions)
        {
            sb.AppendLine(ExplainerHelpers.Explain(condition));
        }

        sb.AppendLine();

        return sb.ToString();
    }
}

public class IsInSharedTimeWindowExplainer : ParametersExplainer<IsInSharedTimeWindowCondition>
{
    public override string Explain(IsInSharedTimeWindowCondition parameters)
    {
        return $"Czy różnica dat między pierwszą obserwacją osobnika powracającego a pierwszą obserwacją osobnika jest mniejsza lub równa niż {parameters.MaxTimeDistanceInDays} dni?";
    }
}