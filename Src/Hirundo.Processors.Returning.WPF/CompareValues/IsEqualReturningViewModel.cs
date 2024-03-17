using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.CompareValues;

[ParametersData(
    typeof(IsEqualReturningCondition),
    typeof(CompareValuesReturningModel<IsEqualReturningCondition>),
    typeof(CompareValuesReturningView),
    "Czy dane są równe?",
    "Osobnik zawiera obserwację z polem równym danej wartości."
)]
public class IsEqualReturningViewModel(CompareValuesReturningModel<IsEqualReturningCondition> model) : CompareValuesReturningViewModel<IsEqualReturningCondition>(model) { }
