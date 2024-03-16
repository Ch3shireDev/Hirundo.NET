using Hirundo.Processors.Statistics.Operations;
using Serilog;

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
        Log.Information("Budowanie procesora statystyk. Liczba operacji: {_statisticalOperationsCount}.", _statisticalOperations.Count);
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

    public IStatisticsProcessorBuilder NewBuilder()
    {
        return new StatisticsProcessorBuilder();
    }
}