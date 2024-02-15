namespace Hirundo.Processors.Computed;

public class ComputedValuesCalculatorBuilder : IComputedValuesCalculatorBuilder
{
    private readonly List<IComputedValuesCalculator> _computedValues = [];

    public IComputedValuesCalculator Build()
    {
        return new CompositeCalculator([.._computedValues]);
    }

    public IComputedValuesCalculatorBuilder WithComputedValues(IList<IComputedValuesCalculator> computedValues)
    {
        _computedValues.AddRange(computedValues);

        return this;
    }
}