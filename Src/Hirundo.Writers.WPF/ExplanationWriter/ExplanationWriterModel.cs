using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Writers.WPF.ExplanationWriter;
public class ExplanationWriterModel(ExplanationWriterParameters parameters, ILabelsRepository repository) : ParametersModel(parameters, repository)
{
    public string Path
    {
        get => parameters.Path;
        set => parameters.Path = value;
    }
}
