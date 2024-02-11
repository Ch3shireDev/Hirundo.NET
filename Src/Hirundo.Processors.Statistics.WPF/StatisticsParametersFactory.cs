using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics.WPF;

public interface IStatisticsParametersFactory : IParametersFactory<IStatisticalOperation>
{
}

public class StatisticsParametersFactory(IDataLabelRepository repository) : ParametersFactory<IStatisticalOperation, StatisticsModel>(repository), IStatisticsParametersFactory
{
}