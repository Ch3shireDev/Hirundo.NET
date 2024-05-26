using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.WPF.Returning.CompareValues;

[ParametersData
(
    typeof(IsLowerOrEqualReturningCondition),
    typeof(CompareValuesReturningModel<IsLowerOrEqualReturningCondition>),
    typeof(CompareValuesReturningView)
)]
public class IsLowerOrEqualReturningViewModel(CompareValuesReturningModel<IsLowerOrEqualReturningCondition> model) : CompareValuesReturningViewModel<IsLowerOrEqualReturningCondition>(model)
{
    public override string ValueDescription => "Wartość mniejsza lub równa";
}