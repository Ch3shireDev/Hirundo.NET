using Hirundo.Commons.Helpers;
using Hirundo.Processors.Population.Conditions;
using System.Globalization;
using System.Text;

namespace Hirundo.Processors.Population;

public class PopulationParameters : ISelfExplainer
{
    public IList<IPopulationCondition> Conditions { get; init; } = [];

    public string Explain()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Konfiguracja populacji:");
        sb.AppendLine(CultureInfo.InvariantCulture, $"Populacja jest dobierana do powracającego osobnika na podstawie {Conditions.Count} warunków.");

        foreach (var condition in Conditions)
        {
            sb.AppendLine(ExplainerHelpers.Explain(condition));
        }

        sb.AppendLine();

        return sb.ToString();
    }
}