using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Computed.WPF.Symmetry;

[ParametersData(
    typeof(SymmetryCalculator),
    typeof(SymmetryModel),
    typeof(SymmetryView)
)]
public class SymmetryViewModel(WingParametersModel<SymmetryCalculator> model) : WingParametersViewModel<SymmetryCalculator>(model)
{
}