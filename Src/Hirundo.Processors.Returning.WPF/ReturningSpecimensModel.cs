using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF;

public class ReturningSpecimensModel
{
    public ReturningSpecimensParameters? ReturningSpecimensParameters { get; set; } = null!;
    public IList<IReturningSpecimenFilter> Conditions => ReturningSpecimensParameters!.Conditions;
}