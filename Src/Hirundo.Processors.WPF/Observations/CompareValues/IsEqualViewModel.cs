using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;
using Hirundo.Processors.Observations.WPF.CompareValues;

namespace Hirundo.Processors.WPF.Observations.CompareValues;

[ParametersData(
    typeof(IsEqualCondition),
    typeof(CompareValuesModel<IsEqualCondition>),
    typeof(CompareValuesView)
)]
public class IsEqualViewModel(CompareValuesModel<IsEqualCondition> model) : CompareValuesViewModel<IsEqualCondition>(model)
{
}