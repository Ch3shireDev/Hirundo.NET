using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics.WPF.Average;

public class AverageModel(AverageOperation operation, IDataLabelRepository repository) : ParametersModel(operation, repository)
{
    public string ValueName
    {
        get => operation.ValueName;
        set => operation.ValueName = value;
    }

    public string ResultPrefixName
    {
        get => operation.ResultPrefixName;
        set => operation.ResultPrefixName = value;
    }

    public double Threshold
    {
        get => operation.Outliers.Threshold;
        set => operation.Outliers.Threshold = value;
    }

    public string UpperBound
    {
        get => operation.Outliers.UpperBound;
        set => operation.Outliers.UpperBound = value;
    }

    public string LowerBound
    {
        get => operation.Outliers.LowerBound;
        set => operation.Outliers.LowerBound = value;
    }

    public bool RejectOutliers
    {
        get => operation.Outliers.RejectOutliers;
        set => operation.Outliers.RejectOutliers = value;
    }

    public DataType ValueType { get; set; }
}