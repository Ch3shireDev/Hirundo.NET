using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Computed.WPF.Pointedness;

[ParametersData(
    typeof(PointednessCalculator),
    typeof(PointednessModel),
    typeof(PointednessView)
)]
public class PointednessViewModel(WingParametersModel<PointednessCalculator> model) : WingParametersViewModel<PointednessCalculator>(model)
{
}