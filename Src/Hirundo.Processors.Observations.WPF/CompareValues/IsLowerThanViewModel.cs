using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

[ParametersData(
    typeof(IsLowerThanCondition),
    typeof(CompareValuesModel<IsLowerThanCondition>),
    typeof(CompareValuesView)
)]
public class IsLowerThanViewModel(CompareValuesModel<IsLowerThanCondition> model) : CompareValuesViewModel<IsLowerThanCondition>(model)
{
    public override string ValueDescription => "Mniejsza niż";
}