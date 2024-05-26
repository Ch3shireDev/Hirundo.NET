using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.WPF.Returning.CompareValues;

[ParametersData
(
    typeof(IsNotEqualReturningCondition),
    typeof(CompareValuesReturningModel<IsNotEqualReturningCondition>),
    typeof(CompareValuesReturningView)
)]
public class IsNotEqualReturningViewModel(CompareValuesReturningModel<IsNotEqualReturningCondition> model) : CompareValuesReturningViewModel<IsNotEqualReturningCondition>(model)
{
}