using System.Globalization;
using Hirundo.Commons;

namespace Hirundo.Processors.Statistics.Operations;

[TypeDescription("Histogram")]
public class HistogramOperation : IStatisticalOperation
{
    public HistogramOperation(string valueName, string resultName, decimal minValue, decimal maxValue, decimal interval = 1)
    {
        ValueName = valueName;
        ResultName = resultName;
        MinValue = minValue;
        MaxValue = maxValue;
        Interval = interval;
    }

    public HistogramOperation()
    {
    }

    public string ValueName { get; set; } = string.Empty;
    public string ResultName { get; set; } = string.Empty;

    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; } = 9;
    public decimal Interval { get; set; } = 1;

    public StatisticalOperationResult GetStatistics(IEnumerable<Specimen> populationData)
    {
        var population = populationData.ToArray();

        var pairs = population
            .Select(specimen => (specimen, Value: GetValue(specimen)))
            .ToArray();

        var populationIds = pairs
            .Where(tuple => tuple.Value != null && IsInRange(tuple.Value.Value))
            .Select(tuple => tuple.specimen.Identifier)
            .ToArray();

        var outliersIds = pairs
            .Where(tuple => tuple.Value != null && !IsInRange(tuple.Value.Value))
            .Select(tuple => tuple.specimen.Identifier)
            .ToArray();

        var emptyIds = pairs
            .Where(tuple => tuple.Value == null)
            .Select(tuple => tuple.specimen.Identifier)
            .ToArray();

        var values2 = pairs
            .Select(tuple => tuple.Value ?? 0m)
            .ToArray();

        var valueLabels = new List<string>();

        for (var x = MinValue; x <= MaxValue; x += Interval)
        {
            var valueStr = x.ToString(CultureInfo.InvariantCulture);
            valueLabels.Add($"{ResultName}-{valueStr}");
        }

        var values = new List<object>();

        for (var x = MinValue; x <= MaxValue; x += Interval)
        {
            var start = x;
            var end = x + Interval;

            var count = values2.Count(value => value >= start && value < end);

            values.Add(count);
        }

        return new StatisticalOperationResult(valueLabels, values, populationIds, emptyIds, outliersIds);
    }

    private decimal? GetValue(Specimen specimen)
    {
        var value = specimen.Observations.First().GetValue(ValueName);
        if (value == null) return null;
        return Convert.ToDecimal(value, CultureInfo.InvariantCulture);
    }

    private bool IsInRange(decimal value)
    {
        return value >= MinValue && value < MaxValue + Interval;
    }
}