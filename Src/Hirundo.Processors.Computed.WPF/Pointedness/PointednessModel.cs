using Hirundo.Commons.Repositories;

namespace Hirundo.Processors.Computed.WPF.Pointedness;

public class PointednessModel(PointednessCalculator parameters, ILabelsRepository repository) : WingParametersModel<PointednessCalculator>(parameters, repository)
{
}
