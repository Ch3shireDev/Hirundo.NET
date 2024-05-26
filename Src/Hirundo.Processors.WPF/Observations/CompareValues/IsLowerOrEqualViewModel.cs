using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;
using Hirundo.Processors.Observations.WPF.CompareValues;

namespace Hirundo.Processors.WPF.Observations.CompareValues;

[ParametersData(
    typeof(IsLowerOrEqualCondition),
    typeof(CompareValuesModel<IsLowerOrEqualCondition>),
    typeof(CompareValuesView)
)]
public class IsLowerOrEqualViewModel(CompareValuesModel<IsLowerOrEqualCondition> model) : CompareValuesViewModel<IsLowerOrEqualCondition>(model)
{
    public override string ValueDescription => "Mniejsza lub równa";
}