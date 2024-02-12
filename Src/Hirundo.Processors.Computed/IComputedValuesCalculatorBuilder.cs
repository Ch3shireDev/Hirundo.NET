namespace Hirundo.Processors.Computed;

public interface IComputedValuesCalculatorBuilder
{
    public IComputedValuesCalculator Build();
    IComputedValuesCalculatorBuilder WithComputedValues(ComputedValuesParameters computedValues);
}