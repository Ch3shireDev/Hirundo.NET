using Hirundo.Commons;

namespace Hirundo.Processors.Computed;

public interface IComputedValuesCalculator
{
    Observation Calculate(Observation observation);
}