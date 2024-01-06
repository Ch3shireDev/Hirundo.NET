namespace Hirundo.Writers.Summary;

/// <summary>
///     Parametry dla <see cref="CsvSummaryWriter" />.
/// </summary>
public class CsvSummaryWriterParameters
{
    /// <summary>
    ///     Lokalizacja pliku wynikowego z danymi.
    /// </summary>
    public string SummaryFilepath { get; set; } = null!;
}