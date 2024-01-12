namespace Hirundo.Processors.Statistics.Operations.Outliers;

public interface IOutliersCondition
{
    bool RejectOutliers { get; }
}