using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population;

public interface IPopulationProcessorBuilder
{
    /// <summary>
    ///     Tworzy obiekt typu <see cref="IPopulationProcessor" />.
    /// </summary>
    /// <returns></returns>
    IPopulationProcessor Build();

    /// <summary>
    ///     Tworzy nowego Budowniczego tego samego typu.
    /// </summary>
    /// <returns></returns>
    IPopulationProcessorBuilder NewBuilder();

    IPopulationProcessorBuilder WithCancellationToken(CancellationToken? token);
    IPopulationProcessorBuilder WithPopulationConditions(IEnumerable<IPopulationCondition> populationConditions);
}