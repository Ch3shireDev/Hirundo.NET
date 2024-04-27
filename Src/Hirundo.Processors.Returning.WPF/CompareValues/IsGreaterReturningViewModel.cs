using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.CompareValues;

[ParametersData
                (
                   typeof(IsGreaterThanReturningCondition),
                   typeof(CompareValuesReturningModel<IsGreaterThanReturningCondition>),
                   typeof(CompareValuesReturningView)
                )]
public class IsGreaterReturningViewModel(CompareValuesReturningModel<IsGreaterThanReturningCondition> model) : CompareValuesReturningViewModel<IsGreaterThanReturningCondition>(model)
{
    public override string ValueDescription => "Wartość większa niż";
}
