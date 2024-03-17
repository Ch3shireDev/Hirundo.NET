using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.CompareValues;

[ParametersData
                (
                   typeof(IsGreaterThanReturningCondition),
                   typeof(CompareValuesReturningModel<IsGreaterThanReturningCondition>),
                   typeof(CompareValuesReturningView),
                   "Czy dane są większe?",
                   "Osobnik zawiera obserwację z polem większym od danej wartości."
                )]
public class IsGreaterReturningViewModel(CompareValuesReturningModel<IsGreaterThanReturningCondition> model) : CompareValuesReturningViewModel<IsGreaterThanReturningCondition>(model)
{
    public override string ValueDescription => "Wartość większa niż";
}
