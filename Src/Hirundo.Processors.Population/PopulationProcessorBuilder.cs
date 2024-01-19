using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population;

/// <summary>
///     Budowniczy obiektów typu <see cref="IPopulationProcessor" />.
/// </summary>
public class PopulationProcessorBuilder
{
    private readonly List<IPopulationConditionBuilder> _conditionBuilders = [];

    /// <summary>
    ///     Tworzy obiekt typu <see cref="IPopulationProcessor" />.
    /// </summary>
    /// <returns></returns>
    public IPopulationProcessor Build()
    {
        var conditionBuilder = new CompositePopulationConditionBuilder([.._conditionBuilders]);
        return new PopulationProcessor(conditionBuilder);
    }

    public PopulationProcessorBuilder WithPopulationConditions(IEnumerable<IPopulationConditionBuilder> populationConditions)
    {
        _conditionBuilders.AddRange(populationConditions);
        return this;
    }
}