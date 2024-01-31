namespace Hirundo.Commons.WPF;

public abstract class ParametersBrowserModel
{
    public abstract string Header { get; }
    public abstract string Title { get; }
    public abstract string Description { get; }
    public abstract string AddParametersCommandText { get; }
    public abstract IList<ParametersData> ParametersDataList { get; }
    public abstract void AddParameters(ParametersData parametersData);
    public abstract IEnumerable<ParametersViewModel> GetParametersViewModels();
}