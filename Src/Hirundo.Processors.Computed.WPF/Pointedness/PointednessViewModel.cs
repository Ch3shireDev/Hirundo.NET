using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Computed.WPF.Pointedness;

[ParametersData(
    typeof(PointednessCalculator),
    typeof(PointednessModel),
    typeof(PointednessView),
    "Ostrość skrzydła",
    "Wartość ostrości wyliczana na podstawie parametrów skrzydła."
)]
public class PointednessViewModel(WingParametersModel<PointednessCalculator> model) : WingParametersViewModel<PointednessCalculator>(model)
{
}
