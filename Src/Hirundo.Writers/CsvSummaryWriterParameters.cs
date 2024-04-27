using Hirundo.Commons;

namespace Hirundo.Writers;

/// <summary>
///     Parametry dla <see cref="CsvSummaryWriter" />.
/// </summary>
[TypeDescription("Csv", "Zapis wyników do pliku .csv", "Zapisuje wyniki do standardowego formatu pliku .csv.")]
public class CsvSummaryWriterParameters : IWriterParameters
{
    /// <summary>
    ///     Lokalizacja pliku wynikowego z danymi.
    /// </summary>
    public string Path { get; set; } = null!;
    public string RingHeaderName { get; set; } = "Ring";
    public string DateFirstSeenHeaderName { get; set; } = "DateFirstSeen";
    public string DateLastSeenHeaderName { get; set; } = "DateLastSeen";
}