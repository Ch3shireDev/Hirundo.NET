namespace Hirundo.Processors.Returning.Conditions;

public class ReturningSpecimensParameters(IList<IReturningSpecimenFilter> conditions)
{
    public IList<IReturningSpecimenFilter> Conditions { get; } = conditions;
}