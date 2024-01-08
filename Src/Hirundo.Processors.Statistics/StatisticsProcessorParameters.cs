using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics;

public class StatisticsProcessorParameters
{
    public IStatisticalOperation[] Operations { get; set; } = [];
    public OutliersProcessorParameters Outliers { get; set; } = null!;
}