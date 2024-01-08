using Hirundo.Commons;

namespace Hirundo.Processors.Statistics.Operations;

/// <summary>
///     Zwraca wartość średnią po danej wartości w populacji. W przypadku napotkania wartości null, pomija.
/// </summary>
/// <param name="valueName">Nazwa parametru po którym jest brana wartość średnia.</param>
/// <param name="resultName">Nazwa parametru wynikowego.</param>
[TypeDescription("Average")]
public class AverageValueOperation(string valueName, string resultName) : IStatisticalOperation
{
    public string ValueName { get; } = valueName;
    public string ResultName { get; } = resultName;

    public StatisticalData GetStatistics(IEnumerable<Specimen> populationData)
    {
        var values = populationData
            .Select(specimen => specimen.Observations.First())
            .Select(observation => observation.GetValue(ValueName))
            .Where(value => value != null)
            .Select(value => value!)
            .ToArray();

        var averageValue = GetAverageValue(values);

        return new StatisticalData
        {
            Name = ResultName,
            Value = averageValue
        };
    }

    private static object GetAverageValue(object[] values)
    {
        var type = values.First().GetType();

        if (type == typeof(int))
        {
            return values.Cast<int>().Average();
        }

        if (type == typeof(double))
        {
            return values.Cast<double>().Average();
        }

        if (type == typeof(float))
        {
            return values.Cast<float>().Average();
        }

        if (type == typeof(decimal))
        {
            return values.Cast<decimal>().Average();
        }

        throw new ArgumentException($"Unsupported type: {type}");
    }
}