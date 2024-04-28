using Hirundo.Commons;

namespace Hirundo.Writers;

[TypeDescription("Explanation", "Zapis wyjaśnienia do pliku .txt", "Zapisuje wyjaśnienia do standardowego formatu pliku .txt.")]
public class ExplanationWriterParameters : IWriterParameters
{
    public string Path { get; set; } = "explanation.txt";
}