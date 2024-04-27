using Hirundo.Commons;
using Hirundo.Commons.Models;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics;

/// <summary>
///     Prosta implementacja procesora statystyk.
/// </summary>
public class StatisticsProcessor(IEnumerable<IStatisticalOperation> statisticalOperations, CancellationToken? token = null) : IStatisticsProcessor
{
    /// <summary>
    ///     Zwraca dane statystyczne wyznaczone na podstawie danych populacji.
    /// </summary>
    /// <param name="populationData">Dane populacji.</param>
    /// <returns></returns>
    public IEnumerable<StatisticalData> GetStatistics(Specimen specimen, IEnumerable<Specimen> populationData)
    {
        ArgumentNullException.ThrowIfNull(populationData);
        token?.ThrowIfCancellationRequested();
        var results = statisticalOperations.Select(operation => operation.GetStatistics(specimen, populationData));
        return results.SelectMany(result => result.Names.Zip(result.Values, (name, value) => new StatisticalData(name, value)));
    }
}