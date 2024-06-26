﻿using Hirundo.Commons.WPF;
using Hirundo.Processors.Computed;

namespace Hirundo.Processors.WPF.Computed.Symmetry;

[ParametersData(
    typeof(SymmetryCalculator),
    typeof(SymmetryModel),
    typeof(SymmetryView)
)]
public class SymmetryViewModel(WingParametersModel<SymmetryCalculator> model) : WingParametersViewModel<SymmetryCalculator>(model)
{
}