using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.WPF.Returning.CompareValues;

[ParametersData(
    typeof(IsGreaterOrEqualReturningCondition),
    typeof(CompareValuesReturningModel<IsGreaterOrEqualReturningCondition>),
    typeof(CompareValuesReturningView)
)]
public class IsGreaterOrEqualReturningViewModel(CompareValuesReturningModel<IsGreaterOrEqualReturningCondition> model) : CompareValuesReturningViewModel<IsGreaterOrEqualReturningCondition>(model)
{
    public override string ValueDescription => "Wartość większa lub równa";
}