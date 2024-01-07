using Hirundo.Commons;

namespace Hirundo.Processors.Statistics.Operations;

public interface IStatisticalOperation
{
    public StatisticalData GetStatistics(IEnumerable<Specimen> populationData);
}