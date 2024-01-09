namespace Hirundo.Filters.Specimens;

public class ReturningSpecimensParameters(IList<IReturningSpecimenFilter> conditions)
{
    public IList<IReturningSpecimenFilter> Conditions { get; } = conditions;
}