using Hirundo.Commons.Models;

namespace Hirundo.Processors.Computed;

public class CompositeCalculator : IComputedValuesCalculator
{
    public IList<IComputedValuesCalculator> Calculators { get; }
    public CancellationToken? CancellationToken { get; }
    public CompositeCalculator(params IComputedValuesCalculator[] calculators) : this(calculators, null) { }
    public CompositeCalculator(IList<IComputedValuesCalculator> calculators, CancellationToken? cancellationToken = null)
    {
        ArgumentNullException.ThrowIfNull(calculators);
        Calculators = calculators;
        CancellationToken = cancellationToken;
    }

    public Observation Calculate(Observation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        CancellationToken?.ThrowIfCancellationRequested();

        foreach (var calculator in Calculators)
        {
            observation = calculator.Calculate(observation);
        }

        return observation;
    }
}