using Hirundo.Commons.Models;

namespace Hirundo.Processors.Statistics.Operations;

public interface IStatisticalOperation
{
    public StatisticalOperationResult GetStatistics(ReturningSpecimen returningSpecimen);
    public StatisticalOperationResult GetStatistics(Specimen specimen, IEnumerable<Specimen> population);
}