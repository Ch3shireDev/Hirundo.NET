using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;

namespace Hirundo.Processors.WPF.Observations.IsNotEmpty;

public class IsNotEmptyModel(IsNotEmptyCondition condition, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository) : ParametersModel(condition, labelsRepository, speciesRepository)
{
    public string ValueName
    {
        get => condition.ValueName;
        set => condition.ValueName = value;
    }
}