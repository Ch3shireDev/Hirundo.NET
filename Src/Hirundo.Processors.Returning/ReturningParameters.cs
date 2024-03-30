using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning;

public class ReturningParameters
{
    public ReturningParameters()
    {
    }

    public ReturningParameters(IList<IReturningSpecimenCondition> conditions)
    {
        Conditions = conditions;
    }

    public IList<IReturningSpecimenCondition> Conditions { get; init; } = [];
}