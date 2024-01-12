using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics;

public class StatisticsProcessorParameters
{
    public IList<IStatisticalOperation> Operations { get; init; } = [];
}