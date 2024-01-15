using Hirundo.Processors.Returning.Conditions;
using Hirundo.Processors.Returning.WPF.AfterTimePeriod;
using Hirundo.Processors.Returning.WPF.NotEarlierThanGivenDateNextYear;

namespace Hirundo.Processors.Returning.WPF;

public static class ReturningSpecimensConditionFactory
{
    public static ReturningSpecimensConditionViewModel Create(IReturningSpecimenFilter condition)
    {
        return condition switch
        {
            ReturnsNotEarlierThanGivenDateNextYearFilter m => new NotEarlierThanGivenDateNextYearViewModel(new NotEarlierThanGivenDateNextYearModel(m)),
            ReturnsAfterTimePeriodFilter m => new AfterTimePeriodViewModel(new AfterTimePeriodModel(m)),
            _ => throw new NotImplementedException()
        };
    }
}