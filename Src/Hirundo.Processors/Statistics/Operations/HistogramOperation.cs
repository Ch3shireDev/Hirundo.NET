﻿using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;
using System.Globalization;
using System.Text;

namespace Hirundo.Processors.Statistics.Operations;

[TypeDescription(
    "Histogram",
    "Histogram",
    "Oblicza histogram dla wybranej wartości."
)]
public class HistogramOperation : IStatisticalOperation, ISelfExplainer
{
    public HistogramOperation(
        string valueName,
        string resultName,
        decimal minValue,
        decimal maxValue,
        decimal interval = 1,
        bool includePopulation = false,
        bool includeDistribution = false)
    {
        ValueName = valueName;
        ResultPrefix = resultName;
        MinValue = minValue;
        MaxValue = maxValue;
        Interval = interval;
        IncludePopulation = includePopulation;
        IncludeDistribution = includeDistribution;
    }

    public HistogramOperation()
    {
    }

    public string ValueName { get; set; } = string.Empty;
    public string ResultPrefix { get; set; } = string.Empty;

    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; } = 9;
    public decimal Interval { get; set; } = 1;

    public bool IncludePopulation { get; set; } = true;
    public bool IncludeDistribution { get; set; } = true;

    public string Explain()
    {
        var sb = new StringBuilder();

        sb.AppendLine(CultureInfo.InvariantCulture, $"Operacja histogramu dla wartości {ValueName}. Wartości dla osobnika populacji są wyliczane na podstawie pierwszej obserwacji.");
        sb.AppendLine(CultureInfo.InvariantCulture, $"Wartości muszą być w przedziale od {MinValue} do {MaxValue}, inaczej są kwalifikowane jako wartości odstajace.");
        sb.AppendLine(CultureInfo.InvariantCulture, $"Wartości są grupowane w interwałach co {Interval}.");
        sb.AppendLine("Wartości wynikowe:");

        for (var x = MinValue; x <= MaxValue; x += Interval)
        {
            var valueStr = x.ToString(CultureInfo.InvariantCulture);
            var label = $"{ResultPrefix}_{valueStr}";
            sb.AppendLine(CultureInfo.InvariantCulture, $"- {label} - liczba wartości z przedziału [{x}, {x + Interval}).");
        }

        if (IncludePopulation)
        {
            sb.AppendLine(CultureInfo.InvariantCulture, $"- {ResultPrefix}_POPULATION - liczba populacji, bez osobnika powracającego.");
        }

        if (IncludeDistribution)
        {
            sb.AppendLine(CultureInfo.InvariantCulture,
                $"- {ResultPrefix}_DISTRIBUTION - część populacji (wraz z osobnikiem powracającym) mająca wartości {ValueName} równe bądź niższe niż osobnik powracający. Słupek histogramu w który wlicza się wartość osobnika powracającego jest dodawany w połowie.");
        }

        return sb.ToString();
    }

    public StatisticalOperationResult GetStatistics(ReturningSpecimen returningSpecimen)
    {
        ArgumentNullException.ThrowIfNull(returningSpecimen, nameof(returningSpecimen));

        return GetStatistics(returningSpecimen.Specimen, returningSpecimen.Population);
    }

    public StatisticalOperationResult GetStatistics(Specimen specimen, IEnumerable<Specimen> population)
    {
        ArgumentNullException.ThrowIfNull(specimen, nameof(specimen));
        ArgumentNullException.ThrowIfNull(population, nameof(population));

        var populationArray = population.ToArray();

        var pairs = populationArray
            .Select(specimen => (specimen, Value: GetValue(specimen)))
            .ToArray();

        var populationIds = pairs
            .Where(tuple => tuple.Value != null && IsInRange(tuple.Value.Value))
            .Select(tuple => tuple.specimen.Ring)
            .ToArray();

        var outliersIds = pairs
            .Where(tuple => tuple.Value != null && !IsInRange(tuple.Value.Value))
            .Select(tuple => tuple.specimen.Ring)
            .ToArray();

        var emptyIds = pairs
            .Where(tuple => tuple.Value == null)
            .Select(tuple => tuple.specimen.Ring)
            .ToArray();

        var nonEmptyDecimalValues = pairs
            .Where(tuple => tuple.Value != null)
            .Select(tuple => tuple.Value ?? 0)
            .Select(Convert.ToDecimal)
            .Where(IsInRange)
            .ToArray();

        var valueLabels = new List<string>();

        for (var x = MinValue; x <= MaxValue; x += Interval)
        {
            var valueStr = x.ToString(CultureInfo.InvariantCulture);
            valueLabels.Add($"{ResultPrefix}_{valueStr}");
        }


        var n = Convert.ToInt32((MaxValue - MinValue) / Interval + 1);

        var intValues = new int[n].ToList();

        for (var i = 0; i < n; i++)
        {
            var count = nonEmptyDecimalValues
                    .Select(GetHistogramIndex)
                    .Count(value => value == i)
                ;

            intValues[i] = count;
        }


        if (IncludePopulation)
        {
            valueLabels.Add($"{ResultPrefix}_POPULATION");
            intValues.Add(populationIds.Length);
        }

        var nullableValues = intValues.Cast<object?>().ToList();

        if (IncludeDistribution)
        {
            valueLabels.Add($"{ResultPrefix}_DISTRIBUTION");
            var result = GetValueDistribution(specimen, intValues);

            nullableValues.Add(result);
        }

        return new StatisticalOperationResult(valueLabels, nullableValues, populationIds, emptyIds, outliersIds);
    }

    private object? GetValueDistribution(Specimen specimen, List<int> values)
    {
        var value = specimen.Observations.First().GetValue(ValueName);
        var decValue = DataTypeHelpers.ConvertValue<decimal>(value);
        if (decValue == null) return null;

        var index = GetHistogramIndex(decValue.Value);

        if (index >= values.Count) return 1.0m;
        if (index < 0) return 0.0m;

        var currentValue = values[index] + 1;
        var sum = values.Take(index).Sum() + currentValue / 2.0m;
        var total = values.Sum() + 1;

        if (total == 0) return null;

        return sum / total;
    }

    private int GetHistogramIndex(decimal value)
    {
        return (int)((value - MinValue) / Interval);
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