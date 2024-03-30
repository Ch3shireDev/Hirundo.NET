using Hirundo.Commons.Repositories;

namespace Hirundo.Processors.Computed.WPF.Pointedness;

public class PointednessModel(PointednessCalculator parameters, IDataLabelRepository repository) : WingParametersModel<PointednessCalculator>(parameters, repository)
{
}
