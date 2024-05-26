using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.WPF.Population.IsEqual;

public class IsEqualPopulationModel(IsEqualPopulationCondition condition, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : ParametersModel(condition, labelsRepository, speciesRepository)
{
    public string ValueName
    {
        get => condition.ValueName;
        set => condition.ValueName = value;
    }

    public string Value
    {
        get => condition.Value?.ToString() ?? "";
        set => condition.Value = value;
    }
}