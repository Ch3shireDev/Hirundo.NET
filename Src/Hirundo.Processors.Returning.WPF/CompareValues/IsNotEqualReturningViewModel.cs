using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.CompareValues;

[ParametersData
(
    typeof(IsNotEqualReturningCondition),
    typeof(CompareValuesReturningModel<IsNotEqualReturningCondition>),
    typeof(CompareValuesReturningView)
)]
public class IsNotEqualReturningViewModel(CompareValuesReturningModel<IsNotEqualReturningCondition> model) : CompareValuesReturningViewModel<IsNotEqualReturningCondition>(model)
{
}