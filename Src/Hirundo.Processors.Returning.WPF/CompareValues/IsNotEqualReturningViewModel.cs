using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.CompareValues;

[ParametersData
    (
       typeof(IsNotEqualReturningCondition),
       typeof(CompareValuesReturningModel<IsNotEqualReturningCondition>),
       typeof(CompareValuesReturningView),
       "Czy dane nie są równe?",
       "Osobnik zawiera obserwację z polem różnym od danej wartości."
    )]
public class IsNotEqualReturningViewModel(CompareValuesReturningModel<IsNotEqualReturningCondition> model) : CompareValuesReturningViewModel<IsNotEqualReturningCondition>(model) { }
