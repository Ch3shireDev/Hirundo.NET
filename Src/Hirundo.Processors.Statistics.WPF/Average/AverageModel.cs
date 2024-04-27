using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics.WPF.Average;

public class AverageModel(AverageOperation operation, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository) : ParametersModel(operation, labelsRepository, speciesRepository)
{
    public string ValueName
    {
        get => operation.ValueName;
        set => operation.ValueName = value;
    }

    public string ResultPrefix
    {
        get => operation.ResultPrefix;
        set => operation.ResultPrefix = value;
    }

    public double Threshold
    {
        get => operation.Outliers.Threshold;
        set => operation.Outliers.Threshold = value;
    }

    public bool RejectOutliers
    {
        get => operation.Outliers.RejectOutliers;
        set => operation.Outliers.RejectOutliers = value;
    }

    public bool AddValueDifference
    {
        get => operation.AddValueDifference;
        set => operation.AddValueDifference = value;
    }

    public bool AddStandardDeviationDifference
    {
        get => operation.AddStandardDeviationDifference;
        set => operation.AddStandardDeviationDifference = value;
    }

    public DataType ValueType { get; set; }
}