using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF;

public class ReturningSpecimensModel
{
    public ReturningSpecimensParameters? ReturningSpecimensParameters { get; set; } = new ReturningSpecimensParameters();
    public IList<IReturningSpecimenFilter> Conditions => ReturningSpecimensParameters!.Conditions;

    public void AddCondition(Type selectedReturningType)
    {
        if (selectedReturningType == typeof(ReturnsAfterTimePeriodFilter))
        {
            Conditions.Add(new ReturnsAfterTimePeriodFilter());
        }
        else if (selectedReturningType == typeof(ReturnsNotEarlierThanGivenDateNextYearFilter))
        {
            Conditions.Add(new ReturnsNotEarlierThanGivenDateNextYearFilter());
        }
        else
        {
            throw new ArgumentException($"Unknown type: {selectedReturningType}", nameof(selectedReturningType));
        }
    }
}