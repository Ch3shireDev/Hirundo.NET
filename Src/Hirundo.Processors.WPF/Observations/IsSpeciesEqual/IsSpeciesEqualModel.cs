using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;

namespace Hirundo.Processors.WPF.Observations.IsSpeciesEqual;

public class IsSpeciesEqualModel(IsSpeciesEqualCondition condition, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersModel(condition, labelsRepository, speciesRepository)
{
    public string Species
    {
        get => condition.Species;
        set => condition.Species = value;
    }
}