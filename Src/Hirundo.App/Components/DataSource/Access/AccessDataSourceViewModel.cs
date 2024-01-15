﻿using Hirundo.Databases;
using Hirundo.Databases.Conditions;

namespace Hirundo.App.Components.DataSource.Access;

public class AccessDataSourceViewModel(AccessDatabaseParameters parameters) : ViewModelBase
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
}