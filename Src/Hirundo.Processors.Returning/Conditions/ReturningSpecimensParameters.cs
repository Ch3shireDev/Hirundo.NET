namespace Hirundo.Processors.Returning.Conditions;

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