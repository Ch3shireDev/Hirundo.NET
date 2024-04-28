using Hirundo.Commons;
using Hirundo.Commons.Helpers;

namespace Hirundo.Writers;

[TypeDescription("Explanation", "Zapis wyjaśnienia do pliku .txt", "Zapisuje wyjaśnienia do standardowego formatu pliku .txt.")]
public class ExplanationWriterParameters : IWriterParameters, ISelfExplainer
{
    public string Explain()
    {
        return $"Zapis wyjaśnienia do pliku tekstowego: {Path}.";
    }

    public string Path { get; set; } = "explanation.txt";
}