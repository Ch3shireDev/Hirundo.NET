using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Computed.WPF.Symmetry;

[ParametersData(
    typeof(SymmetryCalculator),
    typeof(SymmetryModel),
    typeof(SymmetryView),
    "Skośność skrzydła",
    "Wartość skośności wyliczana na podstawie parametrów skrzydła."
)]
public class SymmetryViewModel(WingParametersModel<SymmetryCalculator> model) : WingParametersViewModel<SymmetryCalculator>(model)
{
}
