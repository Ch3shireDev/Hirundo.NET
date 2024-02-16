using Hirundo.Commons.Repositories.Labels;

namespace Hirundo.Processors.Computed.WPF.Pointedness;

public class PointednessModel : WingParametersModel<PointednessCalculator>
{
    public PointednessModel(PointednessCalculator parameters, IDataLabelRepository repository) : base(parameters, repository)
    {
    }
}
