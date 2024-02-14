using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Computed.WPF;

public class ComputedParametersFactory(IDataLabelRepository repository) : ParametersFactory<IComputedValuesCalculator, ComputedValuesModel>(repository), IComputedParametersFactory
{
}