using Hirundo.Commons;
using Hirundo.Commons.Helpers;
using Hirundo.Commons.Models;
using Hirundo.Processors.Statistics.Operations.Outliers;
using System.Globalization;
using System.Text;

namespace Hirundo.Processors.Statistics.Operations;

/// <summary>
///     Zwraca wartość średnią po danej wartości w populacji. W przypadku napotkania wartości null pomija.
/// </summary>
[TypeDescription(
    "AverageAndDeviation",
    "Wartość średnia i odchylenie standardowe",
    "Oblicza wartość średnią i odchylenie standardowe dla wybranej wartości."
)]
public class AverageOperation : IStatisticalOperation, ISelfExplainer
{
    public AverageOperation()
    {
    }

    public AverageOperation(string valueName, string prefixName, bool addValueDifference = false, bool addStandardDeviationDifference = false)
    {
        ValueName = valueName;
        ResultPrefix = prefixName;
        AddValueDifference = addValueDifference;
        AddStandardDeviationDifference = addStandardDeviationDifference;
    }

    public string ValueName { get; set; } = null!;
    public string ResultPrefix { get; set; } = null!;
    public bool AddValueDifference { get; set; } = true;
    public bool AddStandardDeviationDifference { get; set; } = true;

    public StandardDeviationOutliersCondition Outliers { get; set; } = new() { RejectOutliers = false };

    public string Explain()
    {
        var sb = new StringBuilder();

        var accepted = Outliers.RejectOutliers ? "odrzucane" : "wliczane";

        sb.AppendLine(CultureInfo.InvariantCulture, $"Operacja średniej z wartości {ValueName}. Wartości odstające są {accepted}. Wartości dla osobnika populacji są wyliczane na podstawie pierwszej obserwacji.");
        sb.AppendLine("Wartości wynikowe:");
        sb.AppendLine(CultureInfo.InvariantCulture, $"- {ResultPrefix}_AVERAGE - wyliczona średnia.");
        sb.AppendLine(CultureInfo.InvariantCulture, $"- {ResultPrefix}_STANDARD_DEVIATION - wyliczone odchylenie standardowe (po odrzuceniu wartości odstających).");
        sb.AppendLine(CultureInfo.InvariantCulture, $"- {ResultPrefix}_POPULATION_SIZE - liczba wartości z pominięciem wartości pustych oraz odstających. Osobnik powracający nie jest wliczany w populację.");
        sb.AppendLine(CultureInfo.InvariantCulture, $"- {ResultPrefix}_EMPTY_SIZE - liczba pustych wartości.");
        sb.AppendLine(CultureInfo.InvariantCulture, $"- {ResultPrefix}_OUTLIER_SIZE - liczba wartości odstających.");
        sb.AppendLine(CultureInfo.InvariantCulture, $"- {ResultPrefix}_VALUE_DIFFERENCE - różnica pomiędzy wartością cechy osobnika powracającego a wartością średnią - dodatnia, jeśli wartość {ValueName} osobnika powracającego jest większa niż średnia.");
        sb.AppendLine(CultureInfo.InvariantCulture, $"- {ResultPrefix}_STANDARD_DEVIATION_DIFFERENCE - różnica pomiędzy wartością cechy osobnika powracającego a wartością średnią, w odchyleniach standardowych.");

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

        if (populationArray.Length == 0)
        {
            return GetResult(specimen, [null, null], [], [], []);
        }

        var emptyValuesIds = populationArray
            .Where(specimen => specimen.Observations.First().GetValue(ValueName) == null)
            .Select(specimen => specimen.Ring)
            .ToHashSet();

        HashSet<string> outliersIds = [];
        HashSet<string> oldOutliersIds;

        object? averageValue;
        object? standardDeviationValue;

        HashSet<string> calculatedPopulationIds;

        var i = 0;

        do
        {
            i += 1;
            var populationIds = GetPopulationIds(populationArray, emptyValuesIds, outliersIds).ToHashSet();
            var values = GetValues(populationArray, populationIds);
            calculatedPopulationIds = populationIds;

            (averageValue, standardDeviationValue) = GetValues(values);

            if (averageValue == null || standardDeviationValue == null)
            {
                return GetResult(specimen, [null, null], populationIds.ToArray(), emptyValuesIds.ToArray(), outliersIds.ToArray());
            }

            oldOutliersIds = outliersIds;
            outliersIds = [.. Outliers.GetOutliersIds(populationArray, ValueName, averageValue, standardDeviationValue)];
        } while (i < 2);

        return GetResult(specimen, [averageValue, standardDeviationValue], calculatedPopulationIds.ToArray(), emptyValuesIds.ToArray(), outliersIds.ToArray());
    }

    private static string[] GetPopulationIds(Specimen[] population, HashSet<string> emptyValuesIds, HashSet<string> outliersIds)
    {
        return population
            .Select(specimen => specimen.Ring)
            .Where(id => !emptyValuesIds.Contains(id))
            .Where(id => !outliersIds.Contains(id))
            .ToArray();
    }

    private object[] GetValues(Specimen[] population, HashSet<string> populationIds)
    {
        return population
            .Where(specimen => populationIds.Contains(specimen.Ring))
            .Select(specimen => specimen.Observations.First())
            .Select(observation => observation.GetValue(ValueName))
            .Select(value => value!)
            .ToArray();
    }


    private StatisticalOperationResult GetResult(Specimen specimen, object?[] values, object[] populationIds, object[] emptyValuesIds, object[] outliersIds)
    {
        List<string> names =
        [
            $"{ResultPrefix}_AVERAGE",
            $"{ResultPrefix}_STANDARD_DEVIATION",
            $"{ResultPrefix}_POPULATION_SIZE",
            $"{ResultPrefix}_EMPTY_SIZE",
            $"{ResultPrefix}_OUTLIER_SIZE"
        ];

        List<object?> valuesWithPopulation = [.. values, populationIds.Length, emptyValuesIds.Length, outliersIds.Length];

        if (AddValueDifference)
        {
            names.Add($"{ResultPrefix}_VALUE_DIFFERENCE");
            var average = values[0];
            var value = specimen.Observations.First().GetValue(ValueName);
            var difference = ComparisonHelpers.Difference(value, average);
            valuesWithPopulation.Add(difference);
        }

        if (AddStandardDeviationDifference)
        {
            names.Add($"{ResultPrefix}_STANDARD_DEVIATION_DIFFERENCE");
            var average = values[0];
            var value = specimen.Observations.First().GetValue(ValueName);
            var difference = ComparisonHelpers.Difference(value, average);
            var standardDeviation = values[1];
            var sdDifference = ComparisonHelpers.Division(difference, standardDeviation);
            valuesWithPopulation.Add(sdDifference);
        }

        return new StatisticalOperationResult(names, valuesWithPopulation, populationIds, emptyValuesIds, outliersIds);
    }

    private static (object? average, object? standardDeviation) GetValues(object[] values)
    {
        if (values.Length == 0)
        {
            return (null, null);
        }

        var type = values.First().GetType();

        if (type == typeof(int))
        {
            var valuesInt = values.Cast<int>().ToArray();
            var average = valuesInt.Average();
            var sd2 = valuesInt.Select(x => x - average).Select(x => x * x / valuesInt.Length).Sum();
            var sd = Math.Sqrt(sd2);
            return (average, sd);
        }

        if (type == typeof(double))
        {
            var valuesDouble = values.Cast<double>().ToArray();
            var average = valuesDouble.Average();
            var sd2 = valuesDouble.Select(x => x - average).Select(x => x * x / valuesDouble.Length).Sum();
            var sd = Math.Sqrt(sd2);
            return (average, sd);
        }

        if (type == typeof(float))
        {
            var valuesFloat = values.Cast<float>().ToArray();
            var average = valuesFloat.Average();
            var sd2 = valuesFloat.Select(x => x - average).Select(x => x * x / valuesFloat.Length).Sum();
            var sd = Math.Sqrt(sd2);
            return (average, sd);
        }

        if (type == typeof(decimal))
        {
            var valuesDecimal = values.Cast<decimal>().ToArray();
            var average = valuesDecimal.Average();
            var sd2 = valuesDecimal.Select(x => x - average).Select(x => x * x / valuesDecimal.Length).Sum();
            var sd = Convert.ToDecimal(Math.Sqrt((double)sd2));
            return (average, sd);
        }

        throw new ArgumentException($"Unsupported type: {type}");
    }
}