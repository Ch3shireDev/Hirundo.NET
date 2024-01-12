using Hirundo.Commons;
using Hirundo.Processors.Statistics.Operations;
using Hirundo.Processors.Statistics.Operations.Outliers;

namespace Hirundo.Processors.Statistics;

/// <summary>
///     Prosta implementacja procesora statystyk.
/// </summary>
public class StatisticsProcessor(
    IEnumerable<IStatisticalOperation> statisticalOperations,
    IEnumerable<IOutliersCondition> outliersConditions) : IStatisticsProcessor
{
    /// <summary>
    ///     Zwraca dane statystyczne wyznaczone na podstawie danych populacji.
    /// </summary>
    /// <param name="populationData">Dane populacji.</param>
    /// <returns></returns>
    public IEnumerable<StatisticalData> GetStatistics(IEnumerable<Specimen> populationData)
    {
        ArgumentNullException.ThrowIfNull(populationData);
        if (outliersConditions.Any()) throw new NotImplementedException();
        var results =  statisticalOperations.Select(operation => operation.GetStatistics(populationData));
    
        return results.SelectMany(result => result.Names.Zip(result.Values, (name, value) => new StatisticalData(name, value)));
    }
}