using Hirundo.Processors.Statistics.Outliers;

namespace Hirundo.Processors.Statistics;

public class OutliersProcessorParameters
{
    public IList<IOutliersCondition> Conditions { get; init; } = [];
}