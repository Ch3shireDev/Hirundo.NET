using Hirundo.Processors.Population.Conditions;
using Serilog;

namespace Hirundo.Processors.Population;

/// <summary>
///     Budowniczy obiektów typu <see cref="IPopulationProcessor" />.
/// </summary>
public class PopulationProcessorBuilder : IPopulationProcessorBuilder
{
    private readonly List<IPopulationCondition> _conditionBuilders = [];
    private CancellationToken? _cancellationToken;

    /// <summary>
    ///     Tworzy obiekt typu <see cref="IPopulationProcessor" />.
    /// </summary>
    /// <returns></returns>
    public IPopulationProcessor Build()
    {
        Log.Information("Budowanie procesora populacji. Liczba warunków: {_conditionsCount}.", _conditionBuilders.Count);
        var conditionBuilder = new CompositePopulationCondition([.. _conditionBuilders]);
        return new PopulationProcessor(conditionBuilder, _cancellationToken);
    }

    public IPopulationProcessorBuilder NewBuilder()
    {
        return new PopulationProcessorBuilder();
    }

    public IPopulationProcessorBuilder WithCancellationToken(CancellationToken? token)
    {
        _cancellationToken = token;
        return this;
    }

    public IPopulationProcessorBuilder WithPopulationConditions(IEnumerable<IPopulationCondition> populationConditions)
    {
        _conditionBuilders.AddRange(populationConditions);
        return this;
    }
}