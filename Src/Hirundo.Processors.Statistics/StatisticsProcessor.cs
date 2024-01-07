﻿using Hirundo.Commons;
using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics;

/// <summary>
///     Prosta implementacja procesora statystyk.
/// </summary>
public class StatisticsProcessor(params IStatisticalOperation[] statisticalOperations) : IStatisticsProcessor
{
    /// <summary>
    ///     Zwraca dane statystyczne wyznaczone na podstawie danych populacji.
    /// </summary>
    /// <param name="populationsData">Dane populacji.</param>
    /// <returns></returns>
    public StatisticalData GetStatistics(PopulationData populationsData)
    {
        var values = statisticalOperations.Select(operation => operation.GetStatistics(populationsData)).ToArray();

        return new StatisticalData
        {
            Values = values
        };
    }
}