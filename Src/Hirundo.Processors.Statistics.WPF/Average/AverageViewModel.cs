using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Statistics.Operations;
using System.Windows.Input;

namespace Hirundo.Processors.Statistics.WPF.Average;

[ParametersData(
    typeof(AverageOperation),
    typeof(AverageModel),
    typeof(AverageView),
    "Wartość średnia i odchylenie standardowe",
    "Oblicza wartość średnią i odchylenie standardowe dla wybranej wartości."
)]
public class AverageViewModel(AverageModel model) : ParametersViewModel
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
        ResultPrefixName = model.ResultPrefixName == model.ValueName ? value : model.ResultPrefixName;
    }

    public IDataLabelRepository Repository => model.Repository;

    public DataType DataType
    {
        get => model.ValueType;
        set
        {
            model.ValueType = value;
            OnPropertyChanged();
        }
    }

    public string ResultPrefixName
    {
        get => model.ResultPrefixName;
        set
        {
            model.ResultPrefixName = value;
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

    public override ICommand RemoveCommand => new RelayCommand(() => Remove(model.Operation));
}