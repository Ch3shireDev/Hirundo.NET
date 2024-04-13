using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Writers.WPF.ExplanationWriter;
public class ExplanationWriterModel(ExplanationWriterParameters parameters, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository) : ParametersModel(parameters, labelsRepository, speciesRepository)
{
    public string Path
    {
        get => parameters.Path;
        set => parameters.Path = value;
    }
}
