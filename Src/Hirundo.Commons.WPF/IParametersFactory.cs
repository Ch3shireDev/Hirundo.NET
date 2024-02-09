namespace Hirundo.Commons.WPF;

public interface IParametersFactory<TCondition>
{
    IEnumerable<ParametersData> GetParametersData();
    ParametersViewModel CreateViewModel(TCondition condition);
    TCondition CreateCondition(ParametersData parametersData);
}