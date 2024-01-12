using Hirundo.Commons;
using Hirundo.Processors.Statistics.Operations.Outliers;

namespace Hirundo.Processors.Statistics.Operations;

/// <summary>
///     Zwraca wartość średnią po danej wartości w populacji. W przypadku napotkania wartości null pomija.
/// </summary>
[TypeDescription("AverageAndDeviation")]
public class AverageAndDeviationOperation : IStatisticalOperation
{
    public AverageAndDeviationOperation()
    {
    }

    /// <summary>
    ///     Zwraca wartość średnią po danej wartości w populacji. W przypadku napotkania wartości null pomija.
    /// </summary>
    /// <param name="valueName">Nazwa parametru, po którym jest brana wartość średnia.</param>
    /// <param name="resultNameAverage">Nazwa parametru wynikowego.</param>
    /// <param name="resultNameStandardDeviation"></param>
    /// <param name="outliers"></param>
    public AverageAndDeviationOperation(string valueName,
        string resultNameAverage,
        string resultNameStandardDeviation,
        IOutliersCondition outliers)
    {
        ValueName = valueName;
        ResultNameAverage = resultNameAverage;
        ResultNameStandardDeviation = resultNameStandardDeviation;
        Outliers = outliers;
    }


    public AverageAndDeviationOperation(string valueName, string resultNameAverage, string resultNameStandardDeviation)
    {
        ValueName = valueName;
        ResultNameAverage = resultNameAverage;
        ResultNameStandardDeviation = resultNameStandardDeviation;
    }

    public string ValueName { get; init; } = null!;
    public string ResultNameAverage { get; init; } = null!;
    public string ResultNameStandardDeviation { get; init; } = null!;
    public IOutliersCondition Outliers { get; init; } = new NoneOutliersCondition();

    public StatisticalOperationResult GetStatistics(IEnumerable<Specimen> populationData)
    {
        var population = populationData.ToArray();

        var values = population
            .Select(specimen => specimen.Observations.First())
            .Select(observation => observation.GetValue(ValueName))
            .Where(value => value != null)
            .Select(value => value!)
            .ToArray();

        var (averageValue, standardDeviationValue) = GetValues(values);

        var populationIds = population
            .Where(specimen => specimen.Observations.First().GetValue(ValueName) != null)
            .Select(pd => pd.Identifier)
            .ToArray();

        var emptyValuesIds = population
            .Where(specimen => specimen.Observations.First().GetValue(ValueName) == null)
            .Select(specimen => specimen.Identifier)
            .ToArray();

        return new StatisticalOperationResult([ResultNameAverage, ResultNameStandardDeviation], [averageValue, standardDeviationValue], populationIds, emptyValuesIds);
    }

    private static (object average, object standardDeviation) GetValues(object[] values)
    {
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