using Hirundo.Commons.WPF;
using Hirundo.Processors.Computed;

namespace Hirundo.WPF.Processors.Computed.Pointedness;

[ParametersData(
    typeof(PointednessCalculator),
    typeof(PointednessModel),
    typeof(PointednessView)
)]
public class PointednessViewModel(WingParametersModel<PointednessCalculator> model) : WingParametersViewModel<PointednessCalculator>(model)
{
}