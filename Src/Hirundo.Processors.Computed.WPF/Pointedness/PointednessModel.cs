using Hirundo.Commons.Repositories;

namespace Hirundo.Processors.Computed.WPF.Pointedness;

public class PointednessModel(PointednessCalculator parameters, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : WingParametersModel<PointednessCalculator>(parameters, labelsRepository, speciesRepository)
{
}