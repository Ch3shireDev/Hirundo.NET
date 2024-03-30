using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics;

public class StatisticsParameters
{
    public IList<IStatisticalOperation> Operations { get; init; } = [];
}