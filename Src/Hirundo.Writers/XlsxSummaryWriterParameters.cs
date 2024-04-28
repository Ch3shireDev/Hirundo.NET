using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using System.Text;

namespace Hirundo.Writers;

[TypeDescription("Xlsx", "Zapis wyników do pliku .xlsx", "Zapisuje wyniki do pliku Excel .xlsx.")]
public class XlsxSummaryWriterParameters : IWriterParameters, ISelfExplainer
{
    public bool IncludeExplanation { get; set; } = true;
    public string SpreadsheetTitle { get; set; } = "Wyniki";
    public string SpreadsheetSubtitle { get; set; } = "Wyjaśnienia";
    public string RingHeaderName { get; set; } = "Ring";
    public string DateFirstSeenHeaderName { get; set; } = "DateFirstSeen";
    public string DateLastSeenHeaderName { get; set; } = "DateLastSeen";

    public string Explain()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Zapis do pliku Excel: {Path}.");

        if (IncludeExplanation)
        {
            sb.AppendLine("Do pliku dodano wyjaśnienia.");
        }

        return sb.ToString();
    }

    public string Path { get; set; } = null!;
}