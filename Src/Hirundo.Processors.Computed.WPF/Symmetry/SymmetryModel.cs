using Hirundo.Commons.Repositories;

namespace Hirundo.Processors.Computed.WPF.Symmetry;

public class SymmetryModel(SymmetryCalculator parameters, IDataLabelRepository repository) : WingParametersModel<SymmetryCalculator>(parameters, repository)
{
}
