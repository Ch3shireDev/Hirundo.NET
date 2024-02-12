using Hirundo.Commons;

namespace Hirundo.Processors.Computed;

public class ComputedValuesCalculatorBuilder : IComputedValuesCalculatorBuilder
{
    public IComputedValuesCalculator Build()
    {
        return new ComputedValuesCalculator();
    }

    public IComputedValuesCalculatorBuilder WithComputedValues(ComputedValuesParameters computedValues)
    {
        return this;
    }
}

public class ComputedValuesCalculator : IComputedValuesCalculator
{
    public Observation Calculate(Observation observation)
    {
        return observation;
    }
}