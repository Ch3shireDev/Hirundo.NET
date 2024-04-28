using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

[ParametersData(
    typeof(IsNotEqualCondition),
    typeof(CompareValuesModel<IsNotEqualCondition>),
    typeof(CompareValuesView)
)]
public class IsNotEqualViewModel(CompareValuesModel<IsNotEqualCondition> model) : CompareValuesViewModel<IsNotEqualCondition>(model)
{
}