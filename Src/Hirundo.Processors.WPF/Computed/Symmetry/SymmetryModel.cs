using Hirundo.Commons.Repositories;
using Hirundo.Processors.Computed;

namespace Hirundo.Processors.WPF.Computed.Symmetry;

public class SymmetryModel(SymmetryCalculator parameters, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : WingParametersModel<SymmetryCalculator>(parameters, labelsRepository, speciesRepository)
{
}