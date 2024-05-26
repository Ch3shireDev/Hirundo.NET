using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.WPF.Returning.CompareValues;

[ParametersData
(
    typeof(IsLowerThanReturningCondition),
    typeof(CompareValuesReturningModel<IsLowerThanReturningCondition>),
    typeof(CompareValuesReturningView)
)]
public class IsLowerReturningViewModel(CompareValuesReturningModel<IsLowerThanReturningCondition> model) : CompareValuesReturningViewModel<IsLowerThanReturningCondition>(model)
{
    public override string ValueDescription => "Wartość mniejsza niż";
}