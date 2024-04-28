using Hirundo.Commons.Helpers;
using System.Text;

namespace Hirundo.Writers;

public class ResultsParameters : ISelfExplainer
{
    public IList<IWriterParameters> Writers { get; init; } = [];

    public string Explain()
    {
        var sb = new StringBuilder();

        sb.AppendLine("Konfiguracja wyników:");
        sb.AppendLine($"Wyniki są zapisywane do {Writers.Count} plików.");

        foreach (var writer in Writers)
        {
            sb.AppendLine(ExplainerHelpers.Explain(writer));
        }

        return sb.ToString();
    }
}