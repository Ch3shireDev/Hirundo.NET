using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.Population;

/// <summary>
///     Budowniczy obiektów typu <see cref="IPopulationProcessor" />.
/// </summary>
public class PopulationProcessorBuilder : IPopulationProcessorBuilder
{
    private readonly List<IPopulationConditionBuilder> _conditionBuilders = [];
    private CancellationToken? _cancellationToken;

    /// <summary>
    ///     Tworzy obiekt typu <see cref="IPopulationProcessor" />.
    /// </summary>
    /// <returns></returns>
    public IPopulationProcessor Build()
    {
        var conditionBuilder = new CompositePopulationConditionBuilder([.. _conditionBuilders]);
        return new PopulationProcessor(conditionBuilder, _cancellationToken);
    }

    public IPopulationProcessorBuilder WithCancellationToken(CancellationToken? token)
    {
        _cancellationToken = token;
        return this;
    }

    public IPopulationProcessorBuilder WithPopulationConditions(IEnumerable<IPopulationConditionBuilder> populationConditions)
    {
        _conditionBuilders.AddRange(populationConditions);
        return this;
    }
}