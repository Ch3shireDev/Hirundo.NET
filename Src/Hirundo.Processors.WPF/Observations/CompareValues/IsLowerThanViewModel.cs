using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;
using Hirundo.Processors.Observations.WPF.CompareValues;

namespace Hirundo.Processors.WPF.Observations.CompareValues;

[ParametersData(
    typeof(IsLowerThanCondition),
    typeof(CompareValuesModel<IsLowerThanCondition>),
    typeof(CompareValuesView)
)]
public class IsLowerThanViewModel(CompareValuesModel<IsLowerThanCondition> model) : CompareValuesViewModel<IsLowerThanCondition>(model)
{
    public override string ValueDescription => "Mniejsza niż";
}