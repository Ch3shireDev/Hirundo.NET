using Hirundo.Commons;

namespace Hirundo.Writers.Summary;

/// <summary>
///     Parametry dla <see cref="CsvSummaryWriter" />.
/// </summary>
[TypeDescription("Csv")]
public class CsvSummaryWriterParameters : IWriterParameters
{
    /// <summary>
    ///     Lokalizacja pliku wynikowego z danymi.
    /// </summary>
    public string Path { get; set; } = null!;
}