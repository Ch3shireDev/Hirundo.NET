﻿using System.Globalization;
using Hirundo.Commons;

namespace Hirundo.Processors.Statistics.Operations.Outliers;

[TypeDescription("StandardDeviation")]
public class StandardDeviationOutliersCondition : IOutliersCondition
{
    public double Threshold { get; init; } = 3;
    public string UpperBound { get; init; } = "WEIGHT_AVERAGE + (WEIGHT_SD * Threshold)";
    public string LowerBound { get; init; } = "WEIGHT_AVERAGE - (WEIGHT_SD * Threshold)";
    public bool RejectOutliers { get; init; } = true;

    public object[] GetOutliersIds(Specimen[] population, string valueName, object averageValue, object standardDeviationValue)
    {
        ArgumentNullException.ThrowIfNull(population, nameof(population));

        if (!RejectOutliers) return [];

        var upperBound = GetUpperBound(averageValue, standardDeviationValue);
        var lowerBound = GetLowerBound(averageValue, standardDeviationValue);

        var outliersIds = new List<object>();

        foreach (var specimen in population)
        {
            var value = specimen.Observations.First().GetValue(valueName);
            if (value == null) continue;
            if (IsInBounds(value, upperBound, lowerBound)) continue;

            outliersIds.Add(specimen.Identifier);
        }

        return [..outliersIds];
    }

    private static bool IsInBounds(object value, object upperBound, object lowerBound)
    {
        return Convert.ToDouble(value, CultureInfo.InvariantCulture) <= Convert.ToDouble(upperBound, CultureInfo.InvariantCulture) &&
               Convert.ToDouble(value, CultureInfo.InvariantCulture) >= Convert.ToDouble(lowerBound, CultureInfo.InvariantCulture);
    }

    private object GetLowerBound(object averageValue, object standardDeviationValue)
    {
        return Convert.ToDouble(averageValue, CultureInfo.InvariantCulture) - Convert.ToDouble(standardDeviationValue, CultureInfo.InvariantCulture) * Threshold;
    }

    private object GetUpperBound(object averageValue, object standardDeviationValue)
    {
        return Convert.ToDouble(averageValue, CultureInfo.InvariantCulture) + Convert.ToDouble(standardDeviationValue, CultureInfo.InvariantCulture) * Threshold;
    }
}