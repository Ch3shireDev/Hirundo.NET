using Hirundo.Filters.Specimens;

namespace Hirundo.App.Components.ReturningSpecimens;

public class ReturningSpecimensModel
{
    public ReturningSpecimensParameters? ReturningSpecimensParameters { get; set; } = null!;
    public IList<IReturningSpecimenFilter> Conditions => ReturningSpecimensParameters!.Conditions;
}