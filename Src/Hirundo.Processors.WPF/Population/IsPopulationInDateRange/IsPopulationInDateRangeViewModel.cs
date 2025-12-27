using Hirundo.Commons.WPF;
using Hirundo.Processors.Population.Conditions;

namespace Hirundo.Processors.WPF.Population.IsPopulationInDateRange;

[ParametersData(
    typeof(IsPopulationInDateRangeCondition),
    typeof(IsPopulationInDateRangeModel),
    typeof(IsPopulationInDateRangeView)
)]
public class IsPopulationInDateRangeViewModel(IsPopulationInDateRangeModel model) : ParametersViewModel(model)
{
}