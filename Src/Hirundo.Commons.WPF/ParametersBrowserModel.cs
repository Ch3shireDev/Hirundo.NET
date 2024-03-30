﻿using Hirundo.Commons.Repositories;

namespace Hirundo.Commons.WPF;

public interface IParametersBrowserModel
{
    string AddParametersCommandText { get; }
    string Description { get; }
    string Header { get; }
    IList<ParametersData> ParametersDataList { get; }
    string Title { get; }
    Action? Process { get; }
    bool CanProcess { get; }
    string ProcessLabel { get; }

    void AddParameters(ParametersData parametersData);
    IEnumerable<ParametersViewModel> GetParametersViewModels();
}

public abstract class ParametersBrowserModel<TConditionContainer, TCondition, TBrowser> : IParametersBrowserModel
    where TConditionContainer : class, new()
    where TCondition : class
    where TBrowser : IParametersBrowserModel
{
    protected ParametersBrowserModel(IDataLabelRepository repository)
    {
        _factory = new ParametersFactory<TCondition, TBrowser>(repository);
        ParametersDataList = _factory.GetParametersData().ToArray();
    }
    protected readonly ParametersFactory<TCondition, TBrowser> _factory;
    public TConditionContainer ParametersContainer { get; set; } = new();
    public abstract IList<TCondition> Parameters { get; }
    public abstract string Header { get; }
    public abstract string Title { get; }
    public abstract string Description { get; }
    public abstract string AddParametersCommandText { get; }
    public virtual IList<ParametersData> ParametersDataList { get; }
    public virtual Action? Process { get; set; }
    public bool CanProcess => Process is not null;
    public virtual string ProcessLabel => "Przetwarzaj";

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
    public void AddParameters(ParametersData parametersData)
    {
        var writer = _factory.CreateCondition(parametersData);
        Parameters.Add(writer);
    }

    public virtual IEnumerable<ParametersViewModel> GetParametersViewModels()
    {
        return Parameters
                .Select(_factory.CreateViewModel)
                .Select(AddRemovedListener)
            ;
    }
}