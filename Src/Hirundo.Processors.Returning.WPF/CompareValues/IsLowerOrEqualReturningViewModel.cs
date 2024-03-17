using Hirundo.Commons.WPF;
using Hirundo.Processors.Returning.Conditions;

namespace Hirundo.Processors.Returning.WPF.CompareValues;

[ParametersData
          (
             typeof(IsLowerOrEqualReturningCondition),
             typeof(CompareValuesReturningModel<IsLowerOrEqualReturningCondition>),
             typeof(CompareValuesReturningView),
             "Czy dane są mniejsze lub równe?",
             "Osobnik zawiera obserwację z polem mniejszym lub równym danej wartości."
          )]
public class IsLowerOrEqualReturningViewModel(CompareValuesReturningModel<IsLowerOrEqualReturningCondition> model) : CompareValuesReturningViewModel<IsLowerOrEqualReturningCondition>(model)
{
    public override string ValueDescription => "Wartość mniejsza lub równa";
}
