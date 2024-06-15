using Hirundo.Commons.Models;

namespace Hirundo.Processors.Computed;

public interface IComputedValuesCalculator
{
    Observation Calculate(Observation observation);
    public IList<Observation> Calculate(IList<Observation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);
        return observations.Select(Calculate).ToList();
    }
}