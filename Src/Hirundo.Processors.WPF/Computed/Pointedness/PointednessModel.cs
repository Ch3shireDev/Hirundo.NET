using Hirundo.Commons.Repositories;
using Hirundo.Processors.Computed;

namespace Hirundo.Processors.WPF.Computed.Pointedness;

public class PointednessModel(PointednessCalculator parameters, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : WingParametersModel<PointednessCalculator>(parameters, labelsRepository, speciesRepository)
{
}