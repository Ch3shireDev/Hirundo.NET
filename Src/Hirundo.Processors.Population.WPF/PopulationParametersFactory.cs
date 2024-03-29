using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population.WPF;

public class PopulationParametersFactory(IDataLabelRepository repository) : ParametersFactory<IPopulationConditionBuilder, PopulationModel>(repository), IPopulationParametersFactory
{
}

public interface IPopulationParametersFactory : IParametersFactory<IPopulationConditionBuilder>
{
}