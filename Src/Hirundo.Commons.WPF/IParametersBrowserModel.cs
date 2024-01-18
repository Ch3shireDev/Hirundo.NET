namespace Hirundo.Commons.WPF;

public interface IParametersBrowserModel
{
    string Title { get; }
    string Description { get; }
    IList<ParametersData> ParametersDataList { get; }
    void AddParameters(ParametersData parametersData);
    IEnumerable<ParametersViewModel> GetParametersViewModels();
}