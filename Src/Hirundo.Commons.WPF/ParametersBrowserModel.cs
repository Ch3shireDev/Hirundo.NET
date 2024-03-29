namespace Hirundo.Commons.WPF;

public interface IParametersBrowserModel
{
    string AddParametersCommandText { get; }
    string Description { get; }
    string Header { get; }
    IList<ParametersData> ParametersDataList { get; }
    string Title { get; }
    void AddParameters(ParametersData parametersData);
    IEnumerable<ParametersViewModel> GetParametersViewModels();
}

public abstract class ParametersBrowserModel<TConditionContainer, TCondition> : IParametersBrowserModel
    where TCondition : class
    where TConditionContainer : class, new()
{
    public TConditionContainer ParametersContainer { get; set; } = new();
    public abstract IList<TCondition> Parameters { get; }
    public abstract string Header { get; }
    public abstract string Title { get; }
    public abstract string Description { get; }
    public abstract string AddParametersCommandText { get; }
    public abstract IList<ParametersData> ParametersDataList { get; }
    public abstract void AddParameters(ParametersData parametersData);
    public abstract IEnumerable<ParametersViewModel> GetParametersViewModels();


    protected ParametersViewModel AddEventListener(ParametersViewModel viewModel)
    {
        if (viewModel is not IRemovable removable) return viewModel;

        removable.Removed += (_, args) =>
        {
            if (args.Parameters is TCondition writerParametersToRemove)
            {
                Parameters.Remove(writerParametersToRemove);
            }
        };

        return viewModel;
    }
}