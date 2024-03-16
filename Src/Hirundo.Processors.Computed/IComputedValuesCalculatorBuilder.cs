namespace Hirundo.Processors.Computed;

public interface IComputedValuesCalculatorBuilder
{
    public IComputedValuesCalculator Build();
    IComputedValuesCalculatorBuilder WithComputedValues(IList<IComputedValuesCalculator> computedValues);
    IComputedValuesCalculatorBuilder WithCancellationToken(CancellationToken? cancellationToken);

    /// <summary>
    ///     Tworzy nowego Budowniczego tego samego typu.
    /// </summary>
    /// <returns></returns>
    IComputedValuesCalculatorBuilder NewBuilder();
}