using Hirundo.Commons;
using System.Text.Json.Serialization;

namespace Hirundo.Writers;

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

    [JsonIgnore]
    public string Filter { get; set; } = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
    [JsonIgnore]
    public string Title { get; set; } = "Wybierz docelową lokalizację pliku CSV.";
    [JsonIgnore]
    public string DefaultFileName { get; set; } = "results.csv";
}