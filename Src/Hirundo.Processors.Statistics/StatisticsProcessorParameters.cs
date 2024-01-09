using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics;

public class StatisticsProcessorParameters
{
    public IList<IStatisticalOperation> Operations { get; init; } = [];
    public OutliersProcessorParameters Outliers { get; set; } = null!;
}