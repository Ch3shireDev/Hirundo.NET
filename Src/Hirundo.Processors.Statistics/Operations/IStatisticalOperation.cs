using Hirundo.Commons;

namespace Hirundo.Processors.Statistics.Operations;

public interface IStatisticalOperation
{
    public StatisticalDataValue GetStatistics(PopulationData populationData);
}