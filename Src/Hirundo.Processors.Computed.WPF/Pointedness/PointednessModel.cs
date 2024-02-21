using Hirundo.Commons.Repositories.Labels;

namespace Hirundo.Processors.Computed.WPF.Pointedness;

public class PointednessModel(PointednessCalculator parameters, IDataLabelRepository repository) : WingParametersModel<PointednessCalculator>(parameters, repository)
{
}
