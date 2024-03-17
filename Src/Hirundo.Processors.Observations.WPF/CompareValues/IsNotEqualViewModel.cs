using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

[ParametersData(
    typeof(IsNotEqualCondition),
    typeof(CompareValuesModel<IsNotEqualCondition>),
    typeof(CompareValuesView),
    "Czy wartość nie jest równa?",
    "Warunek porównujący pole danych z podaną wartością."
)]
public class IsNotEqualViewModel(CompareValuesModel<IsNotEqualCondition> model) : CompareValuesViewModel<IsNotEqualCondition>(model) { }
