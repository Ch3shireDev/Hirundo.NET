
using Hirundo.Commons;
namespace Hirundo.Writers;

[TypeDescription("Xlsx")]
public class XlsxSummaryWriterParameters : IWriterParameters
{
    public string Path { get; set; } = null!;

    public bool IncludeExplanation { get; set; } = true;

    public string SpreadsheetTitle { get; set; } = "Wyniki";

    public string SpreadsheetSubtitle { get; set; } = "Wyjaśnienia";
}
