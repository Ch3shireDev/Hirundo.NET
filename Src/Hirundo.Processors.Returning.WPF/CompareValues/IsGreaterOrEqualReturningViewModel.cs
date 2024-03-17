using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.CompareValues;

[ParametersData(
    typeof(IsGreaterOrEqualReturningCondition),
    typeof(CompareValuesReturningModel<IsGreaterOrEqualReturningCondition>),
    typeof(CompareValuesReturningView),
    "Czy dane są większe lub równe?",
    "Osobnik zawiera obserwację z polem większym lub równym danej wartości."
    )]
public class IsGreaterOrEqualReturningViewModel(CompareValuesReturningModel<IsGreaterOrEqualReturningCondition> model) : CompareValuesReturningViewModel<IsGreaterOrEqualReturningCondition>(model)
{
    public override string ValueDescription => "Wartość większa lub równa";
}