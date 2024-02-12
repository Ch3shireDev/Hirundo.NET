using Hirundo.Commons;
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
    /// <param name="resultNameAverage">Nazwa parametru wynikowego.</param>
    /// <param name="resultNameStandardDeviation"></param>
    /// <param name="outliers"></param>
    public AverageOperation(string valueName,
        string resultNameAverage,
        string resultNameStandardDeviation,
        StandardDeviationOutliersCondition outliers)
    {
        ValueName = valueName;
        ResultNameAverage = resultNameAverage;
        ResultNameStandardDeviation = resultNameStandardDeviation;
        Outliers = outliers;
    }


    public AverageOperation(string valueName, string resultNameAverage, string resultNameStandardDeviation)
    {
        ValueName = valueName;
        ResultNameAverage = resultNameAverage;
        ResultNameStandardDeviation = resultNameStandardDeviation;
    }

    public string ValueName { get; set; } = null!;
    public string ResultNameAverage { get; set; } = null!;
    public string ResultNameStandardDeviation { get; set; } = null!;
    public StandardDeviationOutliersCondition Outliers { get; set; } = new() { RejectOutliers = false };

    public StatisticalOperationResult GetStatistics(IEnumerable<Specimen> populationData)
    {
        var population = populationData.ToArray();

        if (population.Length == 0)
        {
            return new StatisticalOperationResult([ResultNameAverage, ResultNameStandardDeviation], [null, null], Array.Empty<object>(), Array.Empty<object>(), Array.Empty<object>());
        }

        var emptyValuesIds = population
            .Where(specimen => specimen.Observations.First().GetValue(ValueName) == null)
            .Select(specimen => specimen.Identifier)
            .ToArray();

        object[] outliersIds = [];
        object[] oldOutliersIds;

        object? averageValue;
        object? standardDeviationValue;

        object[] populationIds;

        do
        {
            populationIds = population
                .Select(specimen => specimen.Identifier)
                .Where(id => !emptyValuesIds.Contains(id))
                .Where(id => !outliersIds.Contains(id))
                .ToArray();

            var values = population
                .Where(specimen => populationIds.Contains(specimen.Identifier))
                .Select(specimen => specimen.Observations.First())
                .Select(observation => observation.GetValue(ValueName))
                .Select(value => value!)
                .ToArray();

            (averageValue, standardDeviationValue) = GetValues(values);

            if(averageValue == null || standardDeviationValue == null)
            {
                return new StatisticalOperationResult([ResultNameAverage, ResultNameStandardDeviation], [null, null], populationIds, emptyValuesIds, outliersIds);
            }

            oldOutliersIds = outliersIds;
            outliersIds = Outliers.GetOutliersIds(population, ValueName, averageValue, standardDeviationValue);
        
        
        } while (oldOutliersIds.Length != outliersIds.Length);


        return new StatisticalOperationResult([ResultNameAverage, ResultNameStandardDeviation], [averageValue, standardDeviationValue], populationIds, emptyValuesIds, outliersIds);
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