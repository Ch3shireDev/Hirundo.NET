using System.Text.Json.Serialization;

namespace Hirundo.Writers;

public class XlsxSummaryWriterParameters : IWriterParameters
{
    public string Path { get; set; } = null!;

    public bool IncludeExplanation { get; set; } = true;

    public string SpreadsheetTitle { get; set; } = "Wyniki";

    public string SpreadsheetSubtitle { get; set; } = "Wyjaśnienia";

    [JsonIgnore]
    public string Filter { get; set; } = "XLSX files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
    [JsonIgnore]
    public string Title { get; set; } = "Wybierz docelową lokalizację pliku XLSX.";
    [JsonIgnore]
    public string DefaultFileName { get; set; } = "results.xlsx";
}
