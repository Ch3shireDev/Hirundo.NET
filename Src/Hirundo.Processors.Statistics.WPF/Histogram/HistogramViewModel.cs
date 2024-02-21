﻿using Hirundo.Commons;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics.WPF.Histogram;

[ParametersData(
    typeof(HistogramOperation),
    typeof(HistogramModel),
    typeof(HistogramView),
    "Histogram",
    "Oblicza histogram dla wybranej wartości."
)]
public class HistogramViewModel(HistogramModel model) : ParametersViewModel(model)
{
    public string ValueName
    {
        get => model.ValueName;
        set
        {
            UpdatePrefix(value);
            model.ValueName = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    public string ResultPrefix
    {
        get => model.ResultPrefix;
        set
        {
            model.ResultPrefix = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }

    void UpdatePrefix(string value)
    {
        ResultPrefix = model.ResultPrefix == model.ValueName ? value : model.ResultPrefix;
    }

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