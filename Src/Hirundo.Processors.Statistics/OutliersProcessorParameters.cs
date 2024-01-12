using Hirundo.Processors.Statistics.Operations.Outliers;

namespace Hirundo.Processors.Statistics;

public class OutliersProcessorParameters
{
    public IList<IOutliersCondition> Conditions { get; init; } = [];
}