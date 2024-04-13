using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.IsNotEmpty;

public class IsNotEmptyModel(IsNotEmptyCondition condition, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository) : ParametersModel(condition, labelsRepository, speciesRepository)
{
    public string ValueName
    {
        get => condition.ValueName;
        set => condition.ValueName = value;
    }
}