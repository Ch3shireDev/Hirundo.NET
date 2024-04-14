using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

[ParametersData(
    typeof(IsEqualCondition),
    typeof(CompareValuesModel<IsEqualCondition>),
    typeof(CompareValuesView)
)]
public class IsEqualViewModel(CompareValuesModel<IsEqualCondition> model) : CompareValuesViewModel<IsEqualCondition>(model) { }
