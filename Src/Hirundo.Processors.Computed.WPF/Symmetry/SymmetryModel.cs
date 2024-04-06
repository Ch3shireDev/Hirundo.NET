using Hirundo.Commons.Repositories;

namespace Hirundo.Processors.Computed.WPF.Symmetry;

public class SymmetryModel(SymmetryCalculator parameters, ILabelsRepository repository) : WingParametersModel<SymmetryCalculator>(parameters, repository)
{
}
