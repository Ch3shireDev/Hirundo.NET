using System.Text.Json.Serialization;

namespace Hirundo.Writers;

public class ExplanationWriterParameters : IWriterParameters
{
    public string Path { get; set; } = "explanation.txt";


    [JsonIgnore]
    public string Filter { get; set; } = "Txt files (*.txt)|*.txt|All files (*.*)|*.*";
    [JsonIgnore]
    public string Title { get; set; } = "Wybierz docelową lokalizację pliku tekstowego.";
    [JsonIgnore]
    public string DefaultFileName { get; set; } = "explanation.txt";
}