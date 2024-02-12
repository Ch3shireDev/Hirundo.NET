using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population;

public interface IPopulationProcessorBuilder
{
    /// <summary>
    ///     Tworzy obiekt typu <see cref="IPopulationProcessor" />.
    /// </summary>
    /// <returns></returns>
    IPopulationProcessor Build();

    IPopulationProcessorBuilder WithPopulationConditions(IEnumerable<IPopulationConditionBuilder> populationConditions);
}