namespace Hirundo.Processors.Statistics.WPF.Average;

public class AverageViewModel(AverageModel model) : OperationViewModel
{
    public string ValueName
    {
        get => model.ValueName;
        set
        {
            model.ValueName = value;
            OnPropertyChanged();
        }
    }

    public string ResultNameAverage
    {
        get => model.ResultNameAverage;
        set
        {
            model.ResultNameAverage = value;
            OnPropertyChanged();
        }
    }

    public string ResultNameStandardDeviation
    {
        get => model.ResultNameStandardDeviation;
        set
        {
            model.ResultNameStandardDeviation = value;
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

    public string UpperBound
    {
        get => model.UpperBound;
        set
        {
            model.UpperBound = value;
            OnPropertyChanged();
        }
    }

    public string LowerBound
    {
        get => model.LowerBound;
        set
        {
            model.LowerBound = value;

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
}