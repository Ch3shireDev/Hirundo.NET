using Hirundo.Commons.Models;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics.WPF.Average;

[ParametersData(
    typeof(AverageOperation),
    typeof(AverageModel),
    typeof(AverageView)
)]
public class AverageViewModel(AverageModel model) : ParametersViewModel(model)
{
    public string ValueName
    {
        get => model.ValueName;
        set
        {
            UpdatePrefix(value);
            model.ValueName = value;
            OnPropertyChanged();
        }
    }

    private void UpdatePrefix(string value)
    {
        ResultPrefix = model.ResultPrefix == model.ValueName ? value : model.ResultPrefix;
    }

    public DataType DataType
    {
        get => model.ValueType;
        set
        {
            model.ValueType = value;
            OnPropertyChanged();
        }
    }

    public string ResultPrefix
    {
        get => model.ResultPrefix;
        set
        {
            model.ResultPrefix = value;
            OnPropertyChanged();
        }
    }

    public double Threshold
    {
        get => model.Threshold;
        set
        {
            model.Threshold = value;
            OnPropertyChanged();
        }
    }

    public bool RejectOutliers
    {
        get => model.RejectOutliers;
        set
        {
            model.RejectOutliers = value;
            OnPropertyChanged();
        }
    }

    public bool AddValueDifference
    {
        get => model.AddValueDifference;
        set
        {
            model.AddValueDifference = value;
            OnPropertyChanged();
        }
    }

    public bool AddStandardDeviationDifference
    {
        get => model.AddStandardDeviationDifference;
        set
        {
            model.AddStandardDeviationDifference = value;
            OnPropertyChanged();
        }
    }
}