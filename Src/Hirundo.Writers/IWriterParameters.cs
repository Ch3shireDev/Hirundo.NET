using System.Text.Json.Serialization;

namespace Hirundo.Writers;

public interface IWriterParameters
{
    string Path { get; set; }

    [JsonIgnore]
    public string Filter { get; }
    [JsonIgnore]
    public string Title { get; }
    [JsonIgnore]
    public string DefaultFileName { get; }
}