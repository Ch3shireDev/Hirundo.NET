using Hirundo.Commons.Models;

namespace Hirundo.Processors.Statistics.Operations;

public interface IStatisticalOperation
{
    public StatisticalOperationResult GetStatistics(IEnumerable<Specimen> populationData);
}