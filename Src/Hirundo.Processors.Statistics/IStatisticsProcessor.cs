using Hirundo.Commons;

namespace Hirundo.Processors.Statistics;

/// <summary>
///     Przetwarza dane populacji i wyznacza na ich podstawie dane statystyczne, ustalone przez użytkownika w konfiguracji.
/// </summary>
public interface IStatisticsProcessor
{
    public StatisticalData GetStatistics(PopulationData populationData);
}