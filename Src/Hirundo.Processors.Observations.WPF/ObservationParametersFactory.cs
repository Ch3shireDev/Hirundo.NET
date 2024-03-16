using Hirundo.Commons.Repositories.Labels;
using Hirundo.Commons.WPF;

namespace Hirundo.Processors.Observations.WPF;

public class ObservationParametersFactory(IDataLabelRepository repository) : ParametersFactory<IObservationCondition, ObservationParametersBrowserModel>(repository), IObservationParametersFactory
{
}

public interface IObservationParametersFactory : IParametersFactory<IObservationCondition>
{
}