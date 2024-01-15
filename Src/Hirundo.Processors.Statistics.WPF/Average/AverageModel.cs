using Hirundo.Processors.Statistics.Operations;
using Hirundo.Processors.Statistics.Operations.Outliers;

namespace Hirundo.Processors.Statistics.WPF.Average;

public class AverageModel
{
    public AverageModel()
    {
        Operation = new AverageAndDeviationOperation();
    }

    public AverageModel(AverageAndDeviationOperation operation)
    {
        Operation = operation;
    }

    public AverageAndDeviationOperation Operation
    {
        get =>
            new(ValueName, ResultNameAverage, ResultNameStandardDeviation, new StandardDeviationOutliersCondition
            {
                Threshold = Threshold,
                UpperBound = UpperBound,
                LowerBound = LowerBound,
                RejectOutliers = RejectOutliers
            });
        set
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            ValueName = value.ValueName;
            ResultNameAverage = value.ResultNameAverage;
            ResultNameStandardDeviation = value.ResultNameStandardDeviation;
            Threshold = value.Outliers.Threshold;
            UpperBound = value.Outliers.UpperBound;
            LowerBound = value.Outliers.LowerBound;
            RejectOutliers = value.Outliers.RejectOutliers;
        }
    }

    public string ValueName { get; set; } = null!;
    public string ResultNameAverage { get; set; } = null!;
    public string ResultNameStandardDeviation { get; set; } = null!;
    public double Threshold { get; set; } = 1;
    public string UpperBound { get; set; } = null!;
    public string LowerBound { get; set; } = null!;
    public bool RejectOutliers { get; set; } = true;
}