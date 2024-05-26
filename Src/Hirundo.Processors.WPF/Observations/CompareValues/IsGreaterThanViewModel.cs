using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations;
using Hirundo.Processors.Observations.WPF.CompareValues;

namespace Hirundo.Processors.WPF.Observations.CompareValues;

[ParametersData(
    typeof(IsGreaterThanCondition),
    typeof(CompareValuesModel<IsGreaterThanCondition>),
    typeof(CompareValuesView)
)]
public class IsGreaterThanViewModel(CompareValuesModel<IsGreaterThanCondition> model) : CompareValuesViewModel<IsGreaterThanCondition>(model)
{
    public override string ValueDescription => "Większa niż";
}