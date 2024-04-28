using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

[ParametersData(
    typeof(IsGreaterOrEqualCondition),
    typeof(CompareValuesModel<IsGreaterOrEqualCondition>),
    typeof(CompareValuesView)
)]
public class IsGreaterOrEqualViewModel(CompareValuesModel<IsGreaterOrEqualCondition> model) : CompareValuesViewModel<IsGreaterOrEqualCondition>(model)
{
    public override string ValueDescription => "Większa lub równa";
}