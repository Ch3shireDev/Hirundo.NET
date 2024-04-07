using Hirundo.Commons;
using Hirundo.Commons.Models;
using Hirundo.Processors.Statistics.Operations.Outliers;

namespace Hirundo.Processors.Statistics.Operations;

/// <summary>
///     Zwraca wartość średnią po danej wartości w populacji. W przypadku napotkania wartości null pomija.
/// </summary>
[TypeDescription("AverageAndDeviation")]
public class AverageOperation : IStatisticalOperation
{
    public AverageOperation()
    {
    }

    /// <summary>
    ///     Zwraca wartość średnią po danej wartości w populacji. W przypadku napotkania wartości null pomija.
    /// </summary>
    /// <param name="valueName">Nazwa parametru, po którym jest brana wartość średnia.</param>
    /// <param name="prefixName">Nazwa prefiksu dla wyników.</param>
    /// <param name="outliers"></param>
    public AverageOperation(string valueName,
        string prefixName,
        StandardDeviationOutliersCondition outliers)
    {
        ValueName = valueName;
        ResultPrefix = prefixName;
        Outliers = outliers;
    }


    public AverageOperation(string valueName, string prefixName)
    {
        ValueName = valueName;
        ResultPrefix = prefixName;
    }

    public string ValueName { get; set; } = null!;
    public string ResultPrefix { get; set; } = null!;

    public StandardDeviationOutliersCondition Outliers { get; set; } = new() { RejectOutliers = false };

    public StatisticalOperationResult GetStatistics(IEnumerable<Specimen> populationData)
    {
        var population = populationData.ToArray();

        if (population.Length == 0)
        {
            return GetResult([null, null], [], [], []);
        }

        var emptyValuesIds = population
            .Where(specimen => specimen.Observations.First().GetValue(ValueName) == null)
            .Select(specimen => specimen.Ring)
            .ToHashSet();

        HashSet<string> outliersIds = [];
        HashSet<string> oldOutliersIds;

        object? averageValue;
        object? standardDeviationValue;

        HashSet<string> calculatedPopulationIds;

        do
        {
            var populationIds = GetPopulationIds(population, emptyValuesIds, outliersIds).ToHashSet();
            var values = GetValues(population, populationIds);
            calculatedPopulationIds = populationIds;

            (averageValue, standardDeviationValue) = GetValues(values);

            if (averageValue == null || standardDeviationValue == null)
            {
                return GetResult([null, null], populationIds.ToArray(), emptyValuesIds.ToArray(), outliersIds.ToArray());
            }

            oldOutliersIds = outliersIds;
            outliersIds = [.. Outliers.GetOutliersIds(population, ValueName, averageValue, standardDeviationValue)];


        } while (oldOutliersIds.Count != outliersIds.Count);

        return GetResult([averageValue, standardDeviationValue], calculatedPopulationIds.ToArray(), emptyValuesIds.ToArray(), outliersIds.ToArray());
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


    private StatisticalOperationResult GetResult(object?[] values, object[] populationIds, object[] emptyValuesIds, object[] outliersIds)
    {
        string[] names = [
            $"{ResultPrefix}_AVERAGE",
            $"{ResultPrefix}_STANDARD_DEVIATION",
            $"{ResultPrefix}_POPULATION_SIZE",
            $"{ResultPrefix}_EMPTY_SIZE",
            $"{ResultPrefix}_OUTLIER_SIZE"
            ];

        object?[] valuesWithPopulation = [.. values, populationIds.Length, emptyValuesIds.Length, outliersIds.Length];

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
            var sd2 = valuesInt.Select(x => x - average).Select(x => x * x / (valuesInt.Length - 1)).Sum();
            var sd = Math.Sqrt(sd2);
            return (average, sd);
        }

        if (type == typeof(double))
        {
            var valuesDouble = values.Cast<double>().ToArray();
            var average = valuesDouble.Average();
            var sd2 = valuesDouble.Select(x => x - average).Select(x => x * x / (valuesDouble.Length - 1)).Sum();
            var sd = Math.Sqrt(sd2);
            return (average, sd);
        }

        if (type == typeof(float))
        {
            var valuesFloat = values.Cast<float>().ToArray();
            var average = valuesFloat.Average();
            var sd2 = valuesFloat.Select(x => x - average).Select(x => x * x / (valuesFloat.Length - 1)).Sum();
            var sd = Math.Sqrt(sd2);
            return (average, sd);
        }

        if (type == typeof(decimal))
        {
            var valuesDecimal = values.Cast<decimal>().ToArray();
            var average = valuesDecimal.Average();
            var sd2 = valuesDecimal.Select(x => x - average).Select(x => x * x / (valuesDecimal.Length - 1)).Sum();
            var sd = Convert.ToDecimal(Math.Sqrt((double)sd2));
            return (average, sd);
        }

        throw new ArgumentException($"Unsupported type: {type}");
    }
}