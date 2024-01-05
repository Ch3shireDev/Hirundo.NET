using Hirundo.Commons;

namespace Hirundo.Processors.Statistics;

/// <summary>
///     Prosta implementacja procesora statystyk.
/// </summary>
public class StatisticsProcessor : IStatisticsProcessor
{
    /// <summary>
    ///     Zwraca dane statystyczne wyznaczone na podstawie danych populacji.
    /// </summary>
    /// <param name="populationsData">Dane populacji.</param>
    /// <returns></returns>
    public StatisticalData GetStatistics(PopulationData populationsData)
    {
        return new StatisticalData();
    }
}