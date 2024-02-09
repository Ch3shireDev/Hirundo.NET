using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning;

public class ReturningSpecimensParameters
{
    public ReturningSpecimensParameters()
    {
    }

    public ReturningSpecimensParameters(IList<IReturningSpecimenCondition> conditions)
    {
        Conditions = conditions;
    }

    public IList<IReturningSpecimenCondition> Conditions { get; init; } = [];
}