using Hirundo.App.Components.Observations.IsEqual;
using Hirundo.App.Components.Observations.IsInTimeBlock;
using Hirundo.Filters.Observations;

namespace Hirundo.App.Components.Observations;

public static class ConditionViewModelFactory
{
    public static ConditionViewModel Create(IObservationFilter condition)
    {
        return condition switch
        {
            IsEqualFilter filter => new IsEqualViewModel(new IsEqualModel(filter)),
            IsInTimeBlockFilter filter => new IsInTimeBlockViewModel(new IsInTimeBlockModel(filter)),
            _ => throw new NotImplementedException()
        };
    }
}