namespace Hirundo.Commons.WPF;

public interface IParametersBrowserModel
{
    string Header { get; }
    string Title { get; }
    string Description { get; }
    string AddParametersCommandText { get; }
    IList<ParametersData> ParametersDataList { get; }
    void AddParameters(ParametersData parametersData);
    IEnumerable<ParametersViewModel> GetParametersViewModels();
}