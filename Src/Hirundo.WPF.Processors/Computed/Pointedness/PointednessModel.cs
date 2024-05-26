using Hirundo.Commons.Repositories;
using Hirundo.Processors.Computed;

namespace Hirundo.WPF.Processors.Computed.Pointedness;

public class PointednessModel(PointednessCalculator parameters, ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    : WingParametersModel<PointednessCalculator>(parameters, labelsRepository, speciesRepository)
{
}