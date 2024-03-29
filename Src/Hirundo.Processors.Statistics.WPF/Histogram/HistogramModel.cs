﻿using Hirundo.Commons;
using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics.WPF.Histogram;

public class HistogramModel(HistogramOperation parameters, IDataLabelRepository repository) : ParametersModel(parameters, repository)
{
    public string ValueName
    {
        get => parameters.ValueName;
        set => parameters.ValueName = value;
    }

    public string ResultPrefix
    {
        get => parameters.ResultPrefix;
        set => parameters.ResultPrefix = value;
    }

    public decimal MaxValue
    {
        get => parameters.MaxValue;
        set => parameters.MaxValue = value;
    }

    public decimal MinValue
    {
        get => parameters.MinValue;
        set => parameters.MinValue = value;
    }

    public decimal Interval
    {
        get => parameters.Interval;
        set => parameters.Interval = value;
    }

    public DataType DataType { get; set; }
}