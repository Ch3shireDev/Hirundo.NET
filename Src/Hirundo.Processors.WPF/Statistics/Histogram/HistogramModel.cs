using Hirundo.Commons.Models;
using Hirundo.Commons.Repositories;
using Hirundo.Commons.WPF;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.WPF.Statistics.Histogram;

public class HistogramModel(HistogramOperation parameters, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository) : ParametersModel(parameters, labelsRepository, speciesRepository)
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

    public bool IncludePopulation
    {
        get => parameters.IncludePopulation;
        set => parameters.IncludePopulation = value;
    }

    public bool IncludeDistribution
    {
        get => parameters.IncludeDistribution;
        set => parameters.IncludeDistribution = value;
    }

    public DataType DataType { get; set; }
}