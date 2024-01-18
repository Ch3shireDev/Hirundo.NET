using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations.Conditions;
using Hirundo.Processors.Observations.WPF.IsEqual;
using Hirundo.Processors.Observations.WPF.IsInTimeBlock;

namespace Hirundo.Processors.Observations.WPF;

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