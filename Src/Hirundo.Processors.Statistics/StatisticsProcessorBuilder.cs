using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics;

/// <summary>
///     Budowniczy obiektów typu <see cref="IStatisticsProcessor" />. Pozwala na przetworzenie konfiguracji użytkownika do
///     obiektu typu <see cref="IStatisticsProcessor" />.
/// </summary>
public class StatisticsProcessorBuilder : IStatisticsProcessorBuilder
{
    private readonly List<IStatisticalOperation> _statisticalOperations = [];

    private CancellationToken? _token;
    /// <summary>
    ///     Tworzy obiekt typu <see cref="IStatisticsProcessor" />.
    /// </summary>
    /// <returns></returns>
    public IStatisticsProcessor Build()
    {
        return new StatisticsProcessor(_statisticalOperations, _token);
    }

    public IStatisticsProcessorBuilder WithStatisticsOperations(IEnumerable<IStatisticalOperation> statisticsOperations)
    {
        _statisticalOperations.AddRange(statisticsOperations);
        return this;
    }

    public IStatisticsProcessorBuilder WithCancellationToken(CancellationToken? token)
    {
        _token = token;
        return this;
    }

}