namespace Hirundo.Processors.Returning.Conditions;

public class ReturningSpecimensParameters
{
    public ReturningSpecimensParameters()
    {
    }

    public ReturningSpecimensParameters(IList<IReturningSpecimenFilter> conditions)
    {
        Conditions = conditions;
    }

    public IList<IReturningSpecimenFilter> Conditions { get; init; } = [];
}