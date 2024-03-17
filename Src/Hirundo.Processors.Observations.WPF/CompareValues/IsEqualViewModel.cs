using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

[ParametersData(
    typeof(IsEqualCondition),
    typeof(CompareValuesModel<IsEqualCondition>),
    typeof(CompareValuesView),
    "Czy wartość jest równa?",
    "Warunek porównujący pole danych z podaną wartością."
)]
public class IsEqualViewModel(CompareValuesModel<IsEqualCondition> model) : CompareValuesViewModel<IsEqualCondition>(model) { }
