using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

[ParametersData(
    typeof(IsLowerOrEqualCondition),
    typeof(CompareValuesModel<IsLowerOrEqualCondition>),
    typeof(CompareValuesView)
)]
public class IsLowerOrEqualViewModel(CompareValuesModel<IsLowerOrEqualCondition> model) : CompareValuesViewModel<IsLowerOrEqualCondition>(model)
{
    public override string ValueDescription => "Mniejsza lub równa";
}