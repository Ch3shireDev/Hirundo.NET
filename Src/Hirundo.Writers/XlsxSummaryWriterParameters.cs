using Hirundo.Commons;

namespace Hirundo.Writers;

[TypeDescription("Xlsx", "Zapis wyników do pliku .xlsx", "Zapisuje wyniki do pliku Excel .xlsx.")]
public class XlsxSummaryWriterParameters : IWriterParameters
{
    public bool IncludeExplanation { get; set; } = true;
    public string SpreadsheetTitle { get; set; } = "Wyniki";
    public string SpreadsheetSubtitle { get; set; } = "Wyjaśnienia";
    public string RingHeaderName { get; set; } = "Ring";
    public string DateFirstSeenHeaderName { get; set; } = "DateFirstSeen";
    public string DateLastSeenHeaderName { get; set; } = "DateLastSeen";
    public string Path { get; set; } = null!;
}