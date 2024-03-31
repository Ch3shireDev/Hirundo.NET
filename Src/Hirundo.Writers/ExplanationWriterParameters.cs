using Hirundo.Commons;

namespace Hirundo.Writers;

[TypeDescription("Explanation")]
public class ExplanationWriterParameters : IWriterParameters
{
    public string Path { get; set; } = "explanation.txt";
}
