using Hirundo.Commons.Models;
using Hirundo.Processors.Population;
using Hirundo.Processors.Statistics;

namespace Hirundo.Processors.Summary;

public interface ISummaryProcessorBuilder
{
    SummaryProcessorBuilder WithPopulationProcessor(IPopulationProcessor populationProcessor);
    SummaryProcessorBuilder WithStatisticsProcessor(IStatisticsProcessor statisticsProcessor);
    SummaryProcessorBuilder WithTotalPopulation(Specimen[] totalPopulation);
    ISummaryProcessor Build();
}