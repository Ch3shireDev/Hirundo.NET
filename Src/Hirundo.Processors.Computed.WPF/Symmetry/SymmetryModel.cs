using Hirundo.Commons.Repositories.Labels;

namespace Hirundo.Processors.Computed.WPF.Symmetry;

public class SymmetryModel(SymmetryCalculator parameters, IDataLabelRepository repository) : WingParametersModel<SymmetryCalculator>(parameters, repository)
{
}
