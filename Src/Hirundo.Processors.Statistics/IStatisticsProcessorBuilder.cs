using Hirundo.Processors.Statistics.Operations;

namespace Hirundo.Processors.Statistics;

public interface IStatisticsProcessorBuilder
{
    /// <summary>
    ///     Tworzy obiekt typu <see cref="IStatisticsProcessor" />.
    /// </summary>
    /// <returns></returns>
    IStatisticsProcessor Build();

    IStatisticsProcessorBuilder WithStatisticsOperations(IEnumerable<IStatisticalOperation> statisticsOperations);
    IStatisticsProcessorBuilder WithCancellationToken(CancellationToken? token);

    /// <summary>
    ///     Tworzy nowego Budowniczego tego samego typu.
    /// </summary>
    /// <returns></returns>
    IStatisticsProcessorBuilder NewBuilder();
}