using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF.CompareValues;

[ParametersData(
    typeof(IsGreaterThanCondition),
    typeof(CompareValuesModel<IsGreaterThanCondition>),
    typeof(CompareValuesView)
)]
public class IsGreaterThanViewModel(CompareValuesModel<IsGreaterThanCondition> model) : CompareValuesViewModel<IsGreaterThanCondition>(model)
{
    public override string ValueDescription => "Większa niż";
}