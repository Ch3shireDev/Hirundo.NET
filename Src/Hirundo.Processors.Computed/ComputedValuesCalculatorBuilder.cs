namespace Hirundo.Processors.Computed;

public class ComputedValuesCalculatorBuilder : IComputedValuesCalculatorBuilder
{
    private readonly List<IComputedValuesCalculator> _computedValues = [];
    private CancellationToken? _cancellationToken;

    public IComputedValuesCalculator Build()
    {
        return new CompositeCalculator([.. _computedValues], _cancellationToken);
    }

    public IComputedValuesCalculatorBuilder WithComputedValues(IList<IComputedValuesCalculator> computedValues)
    {
        _computedValues.AddRange(computedValues);

        return this;
    }

    public IComputedValuesCalculatorBuilder WithCancellationToken(CancellationToken? cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return this;
    }
}