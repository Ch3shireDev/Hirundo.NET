using Hirundo.Commons;

namespace Hirundo.Processors.Computed;

public class ComputedValuesParameters
{
    public IList<IComputedValuesCalculator> ComputedValues { get; set; } = [];
}

public class SymmetryCalculator(string resultName, string[] wingParameters, string wingName): IComputedValuesCalculator
{
    public Observation Calculate(Observation observation)
    {
        return new Observation(["SYMMETRY"], [0]);
    }
}