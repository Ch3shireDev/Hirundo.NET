using Hirundo.Commons;

namespace Hirundo.Processors.Statistics.Operations.Outliers;

[TypeDescription("StandardDeviation")]
public class StandardDeviationOutliersCondition : IOutliersCondition
{
    public double Threshold { get; init; } = 3;
    public string UpperBound { get; init; } = "WEIGHT_AVERAGE + (WEIGHT_SD * Threshold)";
    public string LowerBound { get; init; } = "WEIGHT_AVERAGE - (WEIGHT_SD * Threshold)";
    public bool RejectOutliers { get; init; } = true;
}