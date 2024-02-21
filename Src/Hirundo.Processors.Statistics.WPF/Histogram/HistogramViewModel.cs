using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Statistics.Operations;
using System.Windows.Input;

namespace Hirundo.Processors.Statistics.WPF.Histogram;

[ParametersData(
    typeof(HistogramOperation),
    typeof(HistogramModel),
    typeof(HistogramView),
    "Histogram",
    "Oblicza histogram dla wybranej wartości."
)]
public class HistogramViewModel(HistogramModel model) : ParametersViewModel
{
    public string ValueName
    {
        get => model.ValueName;
        set
        {
            model.ValueName = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    public string ResultName
    {
        get => model.ResultName;
        set
        {
            model.ResultName = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    public override ICommand RemoveCommand => new RelayCommand(() => Remove(model.Operation));

    public decimal Interval
    {
        get => model.Interval;
        set
        {
            model.Interval = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    public decimal MinValue
    {
        get => model.MinValue;
        set
        {
            model.MinValue = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    public decimal MaxValue
    {
        get => model.MaxValue;
        set
        {
            model.MaxValue = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    public DataType DataType
    {
        get => model.DataType;
        set
        {
            model.DataType = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    public string ErrorMessage => GetErrorMessage();

    public override IDataLabelRepository Repository => model.Repository;

    private string GetErrorMessage()
    {
        if (string.IsNullOrWhiteSpace(ValueName)) return "";

        if (DataType is DataType.Numeric or DataType.Number)
        {
            if (MinValue > MaxValue)
            {
                return "Minimalna wartość nie może być większa od maksymalnej.";
            }

            if (Interval <= 0)
            {
                return "Interwał musi być większy od zera.";
            }

            return "";
        }

        return $"Dane muszą być typu {DataTypeConverter.ConvertToString(DataType.Numeric)} lub {DataTypeConverter.ConvertToString(DataType.Number)}.";
    }
}