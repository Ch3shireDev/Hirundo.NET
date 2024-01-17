using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics.WPF.Average;

public class AverageModel(AverageOperation operation)
{
    public AverageOperation Operation { get; set; } = operation;

    public string ValueName
    {
        get => Operation.ValueName;
        set => Operation.ValueName = value;
    }

    public string ResultNameAverage
    {
        get => Operation.ResultNameAverage;
        set => Operation.ResultNameAverage = value;
    }

    public string ResultNameStandardDeviation
    {
        get => Operation.ResultNameStandardDeviation;
        set => Operation.ResultNameStandardDeviation = value;
    }

    public double Threshold
    {
        get => Operation.Outliers.Threshold;
        set => Operation.Outliers.Threshold = value;
    }

    public string UpperBound
    {
        get => Operation.Outliers.UpperBound;
        set => Operation.Outliers.UpperBound = value;
    }

    public string LowerBound
    {
        get => Operation.Outliers.LowerBound;
        set => Operation.Outliers.LowerBound = value;
    }

    public bool RejectOutliers
    {
        get => Operation.Outliers.RejectOutliers;
        set => Operation.Outliers.RejectOutliers = value;
    }
}