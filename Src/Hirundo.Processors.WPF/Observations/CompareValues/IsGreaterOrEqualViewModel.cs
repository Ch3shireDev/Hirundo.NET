using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;
using Hirundo.Processors.Observations.WPF.CompareValues;

namespace Hirundo.Processors.WPF.Observations.CompareValues;

[ParametersData(
    typeof(IsGreaterOrEqualCondition),
    typeof(CompareValuesModel<IsGreaterOrEqualCondition>),
    typeof(CompareValuesView)
)]
public class IsGreaterOrEqualViewModel(CompareValuesModel<IsGreaterOrEqualCondition> model) : CompareValuesViewModel<IsGreaterOrEqualCondition>(model)
{
    public override string ValueDescription => "Większa lub równa";
}