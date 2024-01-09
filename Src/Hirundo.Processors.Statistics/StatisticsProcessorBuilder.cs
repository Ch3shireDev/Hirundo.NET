using Hirundo.Processors.Statistics.Operations;
using Hirundo.Processors.Statistics.Outliers;

namespace Hirundo.Processors.Statistics;

/// <summary>
///     Budowniczy obiektów typu <see cref="IStatisticsProcessor" />. Pozwala na przetworzenie konfiguracji użytkownika do
///     obiektu typu <see cref="IStatisticsProcessor" />.
/// </summary>
public class StatisticsProcessorBuilder
{
    private readonly List<IOutliersCondition> _outliersConditions = [];
    private readonly List<IStatisticalOperation> _statisticalOperations = [];

    /// <summary>
    ///     Tworzy obiekt typu <see cref="IStatisticsProcessor" />.
    /// </summary>
    /// <returns></returns>
    public IStatisticsProcessor Build()
    {
        return new StatisticsProcessor(_statisticalOperations, _outliersConditions);
    }

    public StatisticsProcessorBuilder WithOperations(IEnumerable<IStatisticalOperation> statisticsOperations)
    {
        _statisticalOperations.AddRange(statisticsOperations);
        return this;
    }

    public StatisticsProcessorBuilder WithOutliersConditions(IEnumerable<IOutliersCondition> outliersConditions)
    {
        _outliersConditions.AddRange(outliersConditions);
        return this;
    }
}