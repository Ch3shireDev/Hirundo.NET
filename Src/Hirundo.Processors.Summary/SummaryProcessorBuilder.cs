using Hirundo.Commons.Models;
using Hirundo.Processors.Population;
using Hirundo.Processors.Statistics;

namespace Hirundo.Processors.Summary;

/// <summary>
///     Budowniczy dla <see cref="ISummaryProcessor" />. Na podstawie <see cref="IPopulationProcessor" />,
///     <see cref="IStatisticsProcessor" /> oraz dostarczonych danych o wszystkich osobnikach tworzy obiekt
///     <see cref="ISummaryProcessor" />.
/// </summary>
public class SummaryProcessorBuilder : ISummaryProcessorBuilder
{
    private IPopulationProcessor _populationProcessor = null!;
    private IStatisticsProcessor _statisticsProcessor = null!;
    private Specimen[] _totalPopulation = null!;

    public SummaryProcessorBuilder WithPopulationProcessor(IPopulationProcessor populationProcessor)
    {
        _populationProcessor = populationProcessor;
        return this;
    }

    public SummaryProcessorBuilder WithStatisticsProcessor(IStatisticsProcessor statisticsProcessor)
    {
        _statisticsProcessor = statisticsProcessor;
        return this;
    }

    public SummaryProcessorBuilder WithTotalPopulation(Specimen[] totalPopulation)
    {
        _totalPopulation = totalPopulation;
        return this;
    }

    public ISummaryProcessor Build()
    {
        return new SummaryProcessor(_totalPopulation, _populationProcessor, _statisticsProcessor);
    }
}