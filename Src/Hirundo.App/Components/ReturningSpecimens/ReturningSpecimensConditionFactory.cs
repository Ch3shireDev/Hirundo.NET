using Hirundo.App.Components.ReturningSpecimens.AfterTimePeriod;
using Hirundo.App.Components.ReturningSpecimens.NotEarlierThanGivenDateNextYear;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.App.Components.ReturningSpecimens;

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