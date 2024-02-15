using Hirundo.Commons;

namespace Hirundo.Processors.Computed;

public class CompositeCalculator(params IComputedValuesCalculator[] calculators) : IComputedValuesCalculator
{
    public Observation Calculate(Observation observation)
    {
        foreach(var calculator in calculators)
        {
            observation = calculator.Calculate(observation);
        }

        return observation;
    }
}