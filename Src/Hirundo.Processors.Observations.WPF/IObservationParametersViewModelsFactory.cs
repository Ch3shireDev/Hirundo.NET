using Hirundo.Commons.WPF;
using Hirundo.Processors.Observations.Conditions;

namespace Hirundo.Processors.Observations.WPF;

public interface IObservationParametersViewModelsFactory
{
    IEnumerable<ParametersData> GetParametersData();
    ParametersViewModel CreateViewModel(IObservationCondition condition);
    IObservationCondition CreateCondition(ParametersData parametersData);
}