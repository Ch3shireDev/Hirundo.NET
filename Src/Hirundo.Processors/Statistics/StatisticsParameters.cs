using Hirundo.Commons.Helpers;
using Hirundo.Processors.Statistics.Operations;
using System.Globalization;
using System.Text;

namespace Hirundo.Processors.Statistics;

public class StatisticsParameters : ISelfExplainer
{
    public IList<IStatisticalOperation> Operations { get; init; } = [];

    public string Explain()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Konfiguracja statystyk:");
        sb.AppendLine(CultureInfo.InvariantCulture, $"Liczba operacji statystycznych: {Operations.Count}.");
        sb.AppendLine();

        foreach (var statistic in Operations)
        {
            sb.AppendLine(ExplainerHelpers.Explain(statistic));
        }

        return sb.ToString();
    }
}