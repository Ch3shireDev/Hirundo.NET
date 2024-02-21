using Hirundo.Commons;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics.WPF.Average;

[ParametersData(
    typeof(AverageOperation),
    typeof(AverageModel),
    typeof(AverageView),
    "Wartość średnia i odchylenie standardowe",
    "Oblicza wartość średnią i odchylenie standardowe dla wybranej wartości."
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

    void UpdatePrefix(string value)
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
}