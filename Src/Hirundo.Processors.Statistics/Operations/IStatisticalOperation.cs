using Hirundo.Commons;

namespace Hirundo.Processors.Statistics.Operations;

public interface IStatisticalOperation
{
    public StatisticalOperationResult GetStatistics(IEnumerable<Specimen> populationData);
}