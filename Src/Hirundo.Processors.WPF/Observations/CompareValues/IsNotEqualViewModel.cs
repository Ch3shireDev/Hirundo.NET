using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;
using Hirundo.Processors.Observations.WPF.CompareValues;

namespace Hirundo.Processors.WPF.Observations.CompareValues;

[ParametersData(
    typeof(IsNotEqualCondition),
    typeof(CompareValuesModel<IsNotEqualCondition>),
    typeof(CompareValuesView)
)]
public class IsNotEqualViewModel(CompareValuesModel<IsNotEqualCondition> model) : CompareValuesViewModel<IsNotEqualCondition>(model)
{
}