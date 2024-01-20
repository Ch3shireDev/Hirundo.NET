﻿using System.Windows.Input;
using Hirundo.Commons.WPF;
using Hirundo.Commons.WPF.Helpers;
using Hirundo.Databases.Conditions;

namespace Hirundo.Databases.WPF.Access;

public class AccessDataSourceViewModel(AccessDatabaseParameters parameters) : ParametersViewModel, IRemovable, ILabelsUpdater
{
    public string Path
    {
        get => parameters.Path;
        set
        {
            parameters.Path = value;
            OnPropertyChanged();
        }
    }

    public string Table
    {
        get => parameters.Table;
        set
        {
            parameters.Table = value;
            OnPropertyChanged();
        }
    }

    public IList<DatabaseCondition> Conditions => parameters.Conditions;
    public IList<ColumnMapping> Columns => parameters.Columns;
    public ICommand UpdateLabelsCommand => new RelayCommand(UpdateLabels);

    public event EventHandler<EventArgs>? LabelsUpdated;
    public ICommand RemoveCommand => new RelayCommand(RemoveDataSource);

    public event EventHandler<ParametersEventArgs>? Removed;

    private void UpdateLabels()
    {
        LabelsUpdated?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveDataSource()
    {
        Removed?.Invoke(this, new ParametersEventArgs(parameters));
    }
}