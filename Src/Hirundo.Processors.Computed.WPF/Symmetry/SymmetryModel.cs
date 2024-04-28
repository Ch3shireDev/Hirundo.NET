using Hirundo.Commons.Repositories;

namespace Hirundo.Processors.Computed.WPF.Symmetry;

public class SymmetryModel(SymmetryCalculator parameters, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : WingParametersModel<SymmetryCalculator>(parameters, labelsRepository, speciesRepository)
{
}