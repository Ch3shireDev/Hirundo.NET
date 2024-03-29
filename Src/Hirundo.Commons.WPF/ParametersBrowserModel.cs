using Hirundo.Commons.Repositories.Labels;

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
    protected ParametersBrowserModel(IParametersFactory<TCondition> factory)
    {
        _factory = factory;
        ParametersDataList = _factory.GetParametersData().ToArray();
    }
    protected ParametersBrowserModel(IDataLabelRepository repository)
    {
        _factory = new InnerParametersFactory(repository);
        ParametersDataList = _factory.GetParametersData().ToArray();
    }
    protected readonly IParametersFactory<TCondition> _factory;
    public TConditionContainer ParametersContainer { get; set; } = new();
    public abstract IList<TCondition> Parameters { get; }
    public abstract string Header { get; }
    public abstract string Title { get; }
    public abstract string Description { get; }
    public abstract string AddParametersCommandText { get; }
    public virtual IList<ParametersData> ParametersDataList { get; }

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
    public void AddParameters(ParametersData parametersData)
    {
        var writer = _factory.CreateCondition(parametersData);
        Parameters.Add(writer);
    }

    public virtual IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return Parameters
                .Select(_factory.CreateViewModel)
                .Select(AddEventListener)
            ;
    }

    private class InnerParametersFactory(IDataLabelRepository repository) : ParametersFactory<TCondition, ParametersBrowserModel<TConditionContainer, TCondition>>(repository), IParametersFactory<TCondition>
    {
    }
}