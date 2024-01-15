using Hirundo.Writers.Summary;

namespace Hirundo.Writers.WPF.CsvWriter;

public class CsvWriterModel
{
    public CsvWriterModel(CsvSummaryWriterParameters parameters)
    {
        Parameters = parameters;
    }

    public CsvSummaryWriterParameters Parameters
    {
        get => new()
        {
            Path = Path
        };
        set
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            Path = value.Path;
        }
    }

    public string Path { get; set; } = null!;
}