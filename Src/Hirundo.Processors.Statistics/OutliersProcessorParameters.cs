using Hirundo.Processors.Statistics.Outliers;

namespace Hirundo.Processors.Statistics;

public class OutliersProcessorParameters
{
    public IOutliersCondition[] Conditions { get; set; } = [];
}