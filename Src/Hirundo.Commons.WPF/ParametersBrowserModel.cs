using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons.Repositories;
using System.Windows.Input;

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
    IList<CommandData> CommandList { get; }
}

public class CommandData(string commandName, Action commandProcess)
{
    public string CommandName { get; } = commandName;
    public ICommand CommandProcess => new RelayCommand(Command);
    public string CommandResult { get; set; } = string.Empty;

    private void Command()
    {
        commandProcess.Invoke();
    }
}

public abstract class ParametersBrowserModel<TConditionContainer, TCondition, TBrowser> : IParametersBrowserModel
    where TConditionContainer : class, new()
    where TCondition : class
    where TBrowser : IParametersBrowserModel
{
    protected readonly ParametersFactory<TCondition, TBrowser> _factory;

    protected ParametersBrowserModel(ILabelsRepository labelsRepository, ISpeciesRepository speciesRepository)
    {
        _factory = new ParametersFactory<TCondition, TBrowser>(labelsRepository, speciesRepository);
        ParametersDataList = _factory.GetAvailableParameters().ToArray();
    }

    public TConditionContainer ParametersContainer { get; set; } = new();
    public abstract IList<TCondition> Parameters { get; }
    public abstract string Header { get; }
    public abstract string Title { get; }
    public abstract string Description { get; }
    public abstract string AddParametersCommandText { get; }
    public virtual IList<ParametersData> ParametersDataList { get; }

    public virtual IList<CommandData> CommandList { get; } = [];

    public void AddProcess(string label, Action process)
    {
        CommandList.Add(new CommandData(label, process));
    }

    public void AddParameters(ParametersData parametersData)
    {
        var parameters = _factory.CreateCondition(parametersData);
        Parameters.Add(parameters);
    }

    public virtual IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return Parameters
                .Select(_factory.CreateViewModel)
                .Select(AddRemovedListener)
            ;
    }

    protected ParametersViewModel AddRemovedListener(ParametersViewModel viewModel)
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