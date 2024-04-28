using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.IsSpeciesEqual;

public class IsSpeciesEqualModel(IsSpeciesEqualCondition condition, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersModel(condition, labelsRepository, speciesRepository)
{
    public string Species
    {
        get => condition.Species;
        set => condition.Species = value;
    }
}