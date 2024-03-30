using Hirundo.Commons.Helpers;
using Hirundo.Writers.Summary;
using System.Text;

namespace Hirundo.App.Explainers.Writers;
public class ResultsExplainer : ParametersExplainer<ResultsParameters>
{
    public override string Explain(ResultsParameters parameters)
    {
        var sb = new StringBuilder();

        sb.AppendLine("Konfiguracja wyników:");
        sb.AppendLine($"Wyniki są zapisywane do {parameters.Writers.Count} plików.");

        foreach (var writer in parameters.Writers)
        {
            sb.AppendLine(ExplainerHelpers.Explain(writer));
        }

        return sb.ToString();
    }
}

public class CsvWriterExplainer : ParametersExplainer<CsvSummaryWriterParameters>
{
    public override string Explain(CsvSummaryWriterParameters parameters)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Zapis do pliku CSV: {parameters.Path}.");

        return sb.ToString();
    }
}