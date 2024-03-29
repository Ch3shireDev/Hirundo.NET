﻿using CommunityToolkit.Mvvm.Input;
using Hirundo.Commons.WPF;
using Hirundo.Databases.Conditions;
using Serilog;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Hirundo.Databases.WPF.Access;

public class AccessDataSourceViewModel(AccessDataSourceModel model, IAccessMetadataService accessMetadataService) : ParametersViewModel(model), ILabelsUpdater
{
    public override string Name => "Źródło danych Access";
    public override string Description => "Źródło danych z pliku Access.";
    public override string RemoveText => "Usuń źródło danych";

    public string Path
    {
        get => model.Path;
        set
        {
            model.Path = value;
            OnPropertyChanged();
        }
    }

    public string Table
    {
        get => model.Table;
        set
        {
            model.Table = value;
            SetDataColumns(value);
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> DataColumns { get; } = [];
    public ObservableCollection<AccessTableMetadata> MetadatataTables { get; } = [];
    public ObservableCollection<string> Tables { get; } = [];

    public event EventHandler<EventArgs>? LabelsUpdated;


    private void RemoveValues()
    {
        model.Columns.Clear();
        model.Conditions.Clear();

        Columns.Clear();
        Conditions.Clear();

        UpdateLabels();
        OnPropertyChanged(nameof(Columns));
        OnPropertyChanged(nameof(Conditions));
    }

    private void SetDataColumns(string tableName)
    {
        var columns = MetadatataTables
            .FirstOrDefault(table => table.TableName == tableName)
            ?.Columns
            .Select(column => column.ColumnName)
            .ToArray() ?? [];

        DataColumns.Clear();

        foreach (var column in columns)
        {
            DataColumns.Add(column);
        }
    }

    private void UpdateLabels()
    {
        LabelsUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void LoadMetadata(bool force = false)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Path)) return;
            if (!force && Tables.Any()) return;
            var metadata = accessMetadataService.GetTables(Path).ToArray();
            Log.Debug($"Załadowano metadane: {metadata.Length} tabel.");

            var selectedTable = Table;

            Tables.Clear();

            foreach (var table in metadata)
            {
                MetadatataTables.Add(table);
                Tables.Add(table.TableName);
            }

            Table = Tables
                .FirstOrDefault(
                    t => string.Equals(t, selectedTable, StringComparison.InvariantCultureIgnoreCase)
                ) ?? string.Empty;
        }
        catch (Exception e)
        {
            Log.Error(e, $"Błąd podczas ładowania metadanych z pliku {Path}.");

            if (!string.IsNullOrEmpty(Table))
            {
                Tables.Add(Table);
            }
        }
    }

    public void TableSelectionChanged()
    {
        RemoveValues();
    }

    public void AddColumn()
    {
        var newColumnMapping = new ColumnMapping();
        model.Columns.Add(newColumnMapping);
        Columns.Add(newColumnMapping);
    }

    public void RemoveColumn()
    {
        if (Columns.Count <= 0) return;
        var column = Columns.Last();
        model.Columns.Remove(column);
        Columns.Remove(column);
        UpdateLabels();
    }

    public void AddCondition()
    {
        var newCondition = new DatabaseCondition();
        model.Conditions.Add(newCondition);
        Conditions.Add(newCondition);
    }

    public void RemoveCondition()
    {
        if (Conditions.Count <= 0) return;
        var condition = Conditions.Last();
        model.Conditions.Remove(condition);
        Conditions.Remove(condition);
    }

    #region commands

    public ICommand LoadedCommand => new RelayCommand(() => LoadMetadata());
    public ICommand AfterFileCommand => new RelayCommand(() => LoadMetadata(true));
    public ICommand UpdateLabelsCommand => new RelayCommand(UpdateLabels);
    public ICommand RemoveValuesCommand => new RelayCommand(RemoveValues);
    public ICommand TableSelectionChangedCommand => new RelayCommand(TableSelectionChanged);
    public ICommand AddColumnCommand => new RelayCommand(AddColumn);
    public ICommand RemoveColumnCommand => new RelayCommand(RemoveColumn);
    public ICommand AddConditionCommand => new RelayCommand(AddCondition);
    public ICommand RemoveConditionCommand => new RelayCommand(RemoveCondition);

    #endregion

    #region collections

    public IList<ColumnMapping> Columns { get; } = new ObservableCollection<ColumnMapping>(model.Columns);
    public IList<DatabaseCondition> Conditions { get; } = new ObservableCollection<DatabaseCondition>(model.Conditions);

    #endregion
}