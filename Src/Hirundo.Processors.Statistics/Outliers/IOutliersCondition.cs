using Hirundo.Commons;

namespace Hirundo.Processors.Statistics.Outliers;

public interface IOutliersCondition
{
    bool IsOutlier(Specimen specimen);
}