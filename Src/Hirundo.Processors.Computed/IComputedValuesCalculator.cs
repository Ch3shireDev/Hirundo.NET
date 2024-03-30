using Hirundo.Commons.Models;

namespace Hirundo.Processors.Computed;

public interface IComputedValuesCalculator
{
    Observation Calculate(Observation observation);
}